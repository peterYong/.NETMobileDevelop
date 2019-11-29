using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Page2 : CarouselPage
    {
        public Page2()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1(),true);
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page1(),false);
        }
    }
}