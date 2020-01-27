using SkillPool.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillPool.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginView : ContentPage
    {
        public LoginView()
        {
            InitializeComponent();
        }

        private bool _animate;
        protected override async void OnAppearing()
        {
            var content = this.Content;
            this.Content = null;
            this.Content = content;

            LoginViewModel vm = BindingContext as LoginViewModel;
            if (vm != null)
            {
                vm.InvalidateMock();

                if (!vm.IsMock)
                {
                    _animate = true;
                    await AnimateIn(); //正式数据才需要
                }
            }
        }
        public async Task AnimateIn()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                return;
            }
            //await AnimateItem(Banner, 10500); /* Banner是定义在xaml中的图片*/
            await Task.Delay(1000);
        }
        private async Task AnimateItem(View uiElement, uint duration)
        {
            try
            {
                while (_animate)
                {
                    await uiElement.ScaleTo(1.05, duration, Easing.SinInOut);
                    await Task.WhenAll(
                        uiElement.FadeTo(1, duration, Easing.SinInOut),
                        uiElement.LayoutTo(new Rectangle(new Point(0, 0), new Size(uiElement.Width, uiElement.Height))),
                        uiElement.FadeTo(.9, duration, Easing.SinInOut),
                        uiElement.ScaleTo(1.15, duration, Easing.SinInOut)
                    );
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

    }
}
