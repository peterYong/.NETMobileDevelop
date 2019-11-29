using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BaseControls.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Show_Image : ContentPage
    {
        public Show_Image()
        {
            InitializeComponent();

            LoadImg(img1);
            LoadImg(img2);
            LoadImg(img3);
        }

        private async void LoadImg(Image img)
        {
            try
            {
                string url = "https://timgsa.baidu.com/timg?image&quality=80&size=b9999_10000&sec=1571657002284&di=0f6ee8f2c66db769576727092e4591c1&imgtype=0&src=http%3A%2F%2Fimg.mp.itc.cn%2Fupload%2F20160415%2F59cca1073eaa49788e349c67e4a9c37e.jpg";
                HttpClient client = new HttpClient();
                var response = await client.GetAsync(url);
                var imageStrem = await response.Content.ReadAsStreamAsync();

                img.Source = ImageSource.FromStream(() => imageStrem);
            }
            catch (Exception ex)
            {

            }
        }
    }
}