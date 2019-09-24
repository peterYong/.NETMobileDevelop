using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.DataBinding
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BindingPage : ContentPage
    {
        public BindingPage()
        {
            InitializeComponent();
        }

        private async void Btn_BasicCodeBindingPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BasicCodeBindingPage());
        }

        private async void Btn_BasicXamlBindingPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BasicXamlBindingPage());
        }

        private async void Btn_AlternativeCodeBindingPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlternativeCodeBindingPage());
        }

        private async void Btn_AlternativeXamlBindingPage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AlternativeXamlBindingPage());
        }

        private async void Btn_BindingContextInheritancePage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BindingContextInheritancePage());
        }
    }
}