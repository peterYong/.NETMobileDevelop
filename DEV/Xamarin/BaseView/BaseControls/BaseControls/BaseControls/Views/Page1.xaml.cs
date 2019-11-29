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
    public partial class Page1 : TabbedPage
    {
        public Page1()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Page2());
        }

        private void Button1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Page2());
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

        }
    }
}