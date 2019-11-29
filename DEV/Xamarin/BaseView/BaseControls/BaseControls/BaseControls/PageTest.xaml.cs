using BaseControls.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PageTest : ContentPage
    {
        public PageTest()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1());
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page1());
        }
    }
}