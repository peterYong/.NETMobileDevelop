using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CustomRendererWebView
{
    public class HybridWebView : WebView
    {
        Action<string> action;

        /// <summary>
        /// 可绑定属性 Uri
        /// </summary>
        public static readonly BindableProperty UriProperty = BindableProperty.Create(   
            propertyName: nameof(Uri),
            returnType: typeof(string),
            declaringType: typeof(HybridWebView),
            defaultValue: default(string));

        /// <summary>
        /// 指定要加载的网页的地址的 Uri 
        /// </summary>
        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        /// <summary>
        /// 注册方法，
        /// </summary>
        /// <param name="callback">字符串参数的委托</param>
        public void RegisterAction(Action<string> callback)
        {
            action = callback;
        }

        public void Cleanup()
        {
            action = null;
        }

        /// <summary>
        /// 特定平台中触发
        /// </summary>
        /// <param name="data"></param>
        public void InvokeAction(string data)
        {
            if (action == null || data == null)
            {
                return;
            }
            action.Invoke(data);
        }
    }
}
