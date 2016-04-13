using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Foundation;
using WebKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinFormsDemo.Hybrid;
using XamarinFormsDemo.iOS.Renderers;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace XamarinFormsDemo.iOS.Renderers
{
    public class HybridWebViewRenderer : ViewRenderer<HybridWebView, WKWebView>, IWKScriptMessageHandler
    {
        const string JavaScriptFunction = "function invokeCSharpAction(data){window.webkit.messageHandlers.invokeAction.postMessage(data);}";
        WKUserContentController _userController;

        protected override void OnElementChanged(ElementChangedEventArgs<HybridWebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                _userController = new WKUserContentController();
                var script = new WKUserScript(new NSString(JavaScriptFunction), WKUserScriptInjectionTime.AtDocumentEnd, false);
                _userController.AddUserScript(script);
                _userController.AddScriptMessageHandler(this, "invokeAction");

                var config = new WKWebViewConfiguration { UserContentController = _userController };
                var webView = new WKWebView(Frame, config);
                SetNativeControl(webView);
            }
            if (e.OldElement != null)
            {
                _userController.RemoveAllUserScripts();
                _userController.RemoveScriptMessageHandler("invokeAction");
                var hybridWebView = e.OldElement as HybridWebView;
            }
            if (e.NewElement != null)
            {
                string fileName = Path.Combine(NSBundle.MainBundle.BundlePath, $"HybridWeb/{Element.Uri}");

                Control.LoadRequest(new NSUrlRequest(new NSUrl(fileName, false)));
                var hybirdWebView = e.NewElement;
                hybirdWebView.RegisterInvokeJsFunctionAgent((s, action) =>
                {
                    string jsInvokeStr = $"javascript: {s}";
                    Control.EvaluateJavaScript(jsInvokeStr, (rs, error) =>
                    {
                        action?.Invoke(rs.ToString());
                    });
                });

            }
        }

        public void DidReceiveScriptMessage(WKUserContentController userContentController, WKScriptMessage message)
        {
            Element.InvokeAction(message.Body.ToString());
        }
    }
}
