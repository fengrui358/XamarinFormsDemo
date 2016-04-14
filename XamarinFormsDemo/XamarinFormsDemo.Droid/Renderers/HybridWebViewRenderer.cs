using System;
using Android.Net.Http;
using Android.OS;
using Android.Webkit;
using Java.Interop;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using XamarinFormsDemo.Droid.Renderers;
using XamarinFormsDemo.Hybrid;
using View = Android.Views.View;
using WebView = Android.Webkit.WebView;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace XamarinFormsDemo.Droid.Renderers
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, Android.Webkit.WebView>, IDownloadListener, View.IOnLongClickListener
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var webView = new Android.Webkit.WebView(Forms.Context);
                webView.Settings.JavaScriptEnabled = true;
                webView.SetDownloadListener(this);
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                var hybridWebView = e.OldElement as HybridWebView;
                return;
            }
            if (e.NewElement != null)
            {

                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");

                InjectJS(JavaScriptFunction);


                Control.Settings.JavaScriptEnabled = true;
                Control.SetWebChromeClient(new GeoWebChromeClient());
                Control.SetWebViewClient(new MyWebViewClient());
                Control.SetNetworkAvailable(true);
                Control.Settings.SetGeolocationEnabled(true);
                Control.Settings.JavaScriptCanOpenWindowsAutomatically = (true);

                Control.Settings.SetAppCacheEnabled(true);
                Control.Settings.AllowFileAccess = (true);
                Control.Settings.DomStorageEnabled = (true);
                Control.Settings.SetSupportZoom(false);
                Control.Settings.SetSupportMultipleWindows(false);
                Control.Settings.BuiltInZoomControls = (false);
                Control.Settings.SetRenderPriority(WebSettings.RenderPriority.High);

                Control.SetOnLongClickListener(this);
                Control.ClearCache(true);
                if ((int)Build.VERSION.SdkInt >= 19)
                {
                    Control.Settings.LoadsImagesAutomatically = (true);
                }
                else
                {
                    Control.Settings.LoadsImagesAutomatically = (false);
                }



                var hybirdWebView = e.NewElement;
                hybirdWebView.RegisterInvokeJsFunctionAgent((s, action) =>
                {
                    string jsInvokeStr = $"javascript: {s}";

                    // 如果android运行版本高于4.4则调用该版本及其以上所支持的函数
                    if (Build.VERSION.SdkInt >= BuildVersionCodes.Kitkat)
                    {
                        Control.EvaluateJavascript(jsInvokeStr, new ValueCallback(Control));
                    }
                    else
                    {
                        // todo 此处调用本身并不支持有返回值
                        Control.LoadUrl(jsInvokeStr);
                    }

                    //res  http://droidyue.com/blog/2014/09/20/interaction-between-java-and-javascript-in-android/

                    // todo 目前在android还无法实现有返回值
                    if (action != null)
                    {
                        action(string.Empty);
                    }
                });

                Control.LoadUrl($"file:///android_asset/HybridWeb/{Element.Uri}");
            }
        }

        void InjectJS(string script)
        {
            if (Control != null)
            {
                Control.LoadUrl($"javascript: {script}");
            }
        }

        public void OnDownloadStart(string url, string userAgent, string contentDisposition, string mimetype, long contentLength)
        {

        }

        public bool OnLongClick(View v)
        {
            return true;

        }
    }

    public class GeoWebChromeClient : WebChromeClient
    {
        public override void OnGeolocationPermissionsShowPrompt(string origin, GeolocationPermissions.ICallback callback)
        {
            //允许通过权限询问访问
            callback.Invoke(origin, true, false);
        }



    }


    public class MyWebViewClient : WebViewClient
    {
        public override bool ShouldOverrideUrlLoading(WebView view, string url)
        {
            view.LoadUrl(url);
            return true;
        }
        public override void OnPageFinished(WebView view, String url)
        {
            if (!view.Settings.LoadsImagesAutomatically)
            {
                view.Settings.LoadsImagesAutomatically = (true);
            }
        }


        public override void OnReceivedSslError(WebView view, SslErrorHandler handler, SslError error)
        {
            handler.Proceed();
        }
    }

    public class ValueCallback : IValueCallback
    {

        private Android.Webkit.WebView webView;

        public ValueCallback(Android.Webkit.WebView wbView)
        {
            webView = wbView;
        }

        public void OnReceiveValue(Java.Lang.Object value)
        {

        }

        public System.IntPtr Handle
        {
            get { return new IntPtr(); }
        }

        public void Dispose()
        {

        }
    }

    public class JSBridge : Java.Lang.Object
    {
        readonly WeakReference<HybridWebViewRenderer> _hybridWebViewRenderer;

        public JSBridge(HybridWebViewRenderer hybridRenderer)
        {
            _hybridWebViewRenderer = new WeakReference<HybridWebViewRenderer>(hybridRenderer);
        }

        [JavascriptInterface]
        [Export("invokeAction")]
        public void InvokeAction(string data)
        {
            HybridWebViewRenderer hybridRenderer;

            if (_hybridWebViewRenderer != null && _hybridWebViewRenderer.TryGetTarget(out hybridRenderer))
            {
                hybridRenderer.Element.InvokeAction(data);
            }
        }
    }
}