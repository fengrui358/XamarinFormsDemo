using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace XamarinFormsDemo.Hybrid
{
    public class HybridWebView : View
    {
        #region 字段
        private Dictionary<string, Action<object[]>> _actions;
        private Action<string, Action<string>> _invokeJsFunctionAgent;
        #endregion

        #region 属性

        public static readonly BindableProperty UriProperty =
            BindableProperty.Create("Uri", typeof(string), typeof(HybridWebView), default(string));


        /// <summary>
        /// 页面首页Uri
        /// </summary>
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }


        #endregion

        #region 构造


        public HybridWebView()
        {
            _actions = new Dictionary<string, Action<object[]>>();

        }
        #endregion

        #region 方法

        #region 公共方法

        #region 注册net调用js代理方法
        /// <summary>
        /// 注册net调用js代理方法
        /// </summary>
        /// <param name="invokeJsFunc"></param>
        public void RegisterInvokeJsFunctionAgent(Action<string, Action<string>> invokeJsFunc)
        {
            try
            {
                _invokeJsFunctionAgent = invokeJsFunc;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region 调用js函数
        public void InvokeJsFunction(string functionName, Action<string> callback, params object[] functionParams)
        {
            try
            {
                if (_invokeJsFunctionAgent != null)
                    _invokeJsFunctionAgent(BuildInvokeJsString(functionName, functionParams), callback);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region 注册js调用net函数代理
        /// <summary>
        /// 注册js调用net函数代理
        /// </summary>
        /// <param name="callback"></param>
        public void RegisterAction(string functionName, Action<object[]> action)
        {
            try
            {
                if (_actions.ContainsKey(functionName))
                {
                    _actions[functionName] = action;
                }
                else
                {
                    _actions.Add(functionName, action);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        #endregion

        #region js调用net
        /// <summary>
        /// js调用net
        /// </summary>
        /// <param name="data"></param>
        /// <remarks>data应是由函数名和函数参数组成的json字符串</remarks>
        public object InvokeAction(string data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data))
                {
                    var invokerInfoWarp = JsonConvert.DeserializeObject<InvokerInfoWarp>(data);

                    Action<object[]> action = null;
                    _actions.TryGetValue(invokerInfoWarp.FunctionName, out action);

                    action?.Invoke(invokerInfoWarp.FunctionParams);
                }
                return null;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return null;
            }
        }
        #endregion

        #endregion


        #region 私有方法


        #region 构建net调用js函数字符串

        private string BuildInvokeJsString(string functionName, params object[] functionNarams)
        {
            var paramsStr = new StringBuilder();

            #region 构建参数
            if (functionNarams != null)
            {
                for (int i = 0; i < functionNarams.Length; i++)
                {
                    if (i == 0)
                    {
                        paramsStr.AppendFormat("{0}", functionNarams[i]);
                    }
                    else
                    {
                        paramsStr.AppendFormat(",{0}", functionNarams[i]);
                    }
                }
            }
            #endregion

            #region 组装
            var sb = new StringBuilder();
            sb.AppendFormat("{0}({1})", functionName, paramsStr);
            #endregion

            return sb.ToString();
        }
        #endregion

        #endregion

        #endregion
    }

    public class InvokerInfoWarp
    {
        #region 字段

        private string _functionName;
        private object[] _functionParams;

        #endregion


        #region 属性
        public string FunctionName
        {
            get { return _functionName; }
            set { _functionName = value; }
        }

        public object[] FunctionParams
        {
            get { return _functionParams; }
            set { _functionParams = value; }
        }
        #endregion


    }
}
