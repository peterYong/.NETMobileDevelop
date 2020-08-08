using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using FromPractice.Droid.Renderers;
using FromPractice.Renderers;

[assembly: Xamarin.Forms.ExportRenderer(typeof(MyEditorWebViewAndroid), typeof(MyEditorWebView))]
namespace FromPractice.Droid.Renderers
{
    public class MyEditorWebViewAndroid : WebView
    {
        public MyEditorWebViewAndroid(Context context)
       : base(context)
        {
        }

        public MyEditorWebViewAndroid(Context context, IAttributeSet attrs)
            : base(context, attrs)
        {
        }

        public MyEditorWebViewAndroid(Context context, IAttributeSet attrs, int defStyleAttr)
            : base(context, attrs, defStyleAttr)
        {
        }
    }
}