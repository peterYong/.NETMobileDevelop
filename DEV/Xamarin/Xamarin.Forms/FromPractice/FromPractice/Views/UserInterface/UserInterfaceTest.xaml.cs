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
    public partial class UserInterfaceTest : ContentPage
    {
        public UserInterfaceTest()
        {
            InitializeComponent();
        }

        private void Btn_WebViewTestPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WebViewTestPage());
        }

        private void Btn_WebViewTestStringPage_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WebViewTestStringPage());
        }

        private void Btn_PickerTest_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new PickerTest());
        }
    }
}