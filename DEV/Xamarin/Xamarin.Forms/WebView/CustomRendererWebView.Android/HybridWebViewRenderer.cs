using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using CustomRendererWebView;
using CustomRendererWebView.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]
namespace CustomRendererWebView.Droid
{
    //自定义呈现器创建本机 Web 控件并加载 HybridWebView.Uri 属性指定的 HTML 文件。
    //加载网页后，自定义呈现器将 invokeCSharpAction JavaScript 函数注入到网页中。
    //当用户输入其名称并单击 HTML button 元素时，会调用 invokeCSCode 函数，并随之调用 invokeCSharpAction 函数。
    //invokeCSharpAction 函数调用自定义呈现器中的方法，该方法随之调用 HybridWebView.InvokeAction 方法。
    //HybridWebView.InvokeAction 方法调用已注册的 Action。



    public class HybridWebViewRenderer : WebViewRenderer
    {
        Context _context;
        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                ((HybridWebView)Element).Cleanup();
            }
            if (e.NewElement != null)
            {
                //将Uri属性中指定的网页加载到本机 WebView 控件中，并在网页加载完成后 
                //使用 JavascriptWebViewClient 类中 OnPageFinished 的替代方法将  JavaScript函数 invokeCSharpAction 注入到网页中：

                Control.SetWebViewClient(new JavascriptWebViewClient($"javascript: {JavascriptFunction}"));

                //将新的 JSBridge 实例注入到 WebView 的 JavaScript 上下文的主框架中，并将其命名为 jsBridge。 这允许从 JavaScript 访问 JSBridge 类中的方法。
                Control.AddJavascriptInterface(new JSBridge(this), "jsBridge");

                //string uri = ((HybridWebView)Element).Uri;
                //Control.LoadUrl("file:///android_asset/Content/" + uri);

                //在真机上调试才行，模拟机上google打不开html文件中jQuery【$】的网站
                Control.LoadUrl($"file:///android_asset/Content/{((HybridWebView)Element).Uri}");
            }
        }
    }
}