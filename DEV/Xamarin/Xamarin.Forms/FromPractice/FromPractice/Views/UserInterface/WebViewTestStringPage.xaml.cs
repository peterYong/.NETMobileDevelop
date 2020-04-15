using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.UserInterface
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WebViewTestStringPage : ContentPage
    {
        public WebViewTestStringPage()
        {
            InitializeComponent();

            htmlWebString.Html = @"<html><body>
  <h1>Xamarin.Forms</h1>
  <p>Welcome to WebView.</p>
  </body></html>";
        }
    }
}