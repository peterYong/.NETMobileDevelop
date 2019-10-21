using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.Image
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ImageBasicTest : ContentPage
    {
        public ImageBasicTest()
        {
            InitializeComponent();
        }

        private void Btn_RemoteUriImage_Clicked(object sender, EventArgs e)
        {
           Application.Current.MainPage= new NavigationPage(new RemoteUriImage());
        }
    }
}