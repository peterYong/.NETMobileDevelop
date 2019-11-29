using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LocalResource
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Received : ContentPage
    {
        public Received()
        {
            InitializeComponent();
        }


        private void pickerLang_SelectedIndexChanged(object sender, EventArgs e)
        {
            var lang = pickerLang.SelectedItem?.ToString();
            App.Localize(lang);
        }

        private void pickerTheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            var theme = pickerTheme.SelectedItem?.ToString();
            App.Theme(theme);
        }
    }
}