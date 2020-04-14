using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.Layout
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LayoutTest : ContentPage
    {
        public LayoutTest()
        {
            InitializeComponent();
        }

        private async void Btn_AbsoluteLayoutTestPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AbsoluteLayoutTestPage());
        }

        private async void Btn_StackLayoutTestPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StackLayoutTestPage());
        }
    }
}