using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CustomRendererWebView
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();


            //WebView控件中真正需要调用的Action方法是：
            //调用 DisplayAlert 方法以显示模式弹出项，该弹出项 显示在 HybridWebView 实例显示的 HTML 页面中输入的名称。
            hybridWebView.RegisterAction(data => DisplayAlert("Alert", "Hello " + data, "Ok"));
        }
    }
}
