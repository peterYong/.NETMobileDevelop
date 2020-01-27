using SkillPool.Core.Navigation;
using SkillPool.Core.Services.Skilled;
using SkillPool.Core.ViewModels;
using SkillPool.DataAccess.Skilled;
using SkillPool.Model.Skilled;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SkillPool.Core.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SkilledSaveView : ContentPage
    {
        private readonly ISkilledService _skilledService;

        public SkilledSaveView()
        {
            InitializeComponent();

            _skilledService = new SkilledSqliteMockService();
        }

        private async void ButtonAdd_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is SkilledSaveViewModel)
            {
                SkilledItem skilledItem = ((SkilledSaveViewModel)BindingContext).SkilledItem;

                await _skilledService.AddSkilledItemAsync(skilledItem);

                await new NavigationService(null).NavigateToAsync<MainViewModel>();  //导航回主页
                //await new NavigationService(null).NavigateToAsync<MainViewModel>(new TabParameter { TabIndex = 1 });  //TabIndex可以控制导航到哪个页面
            }
        }


        private async void ButtonUpdate_Clicked(object sender, EventArgs e)
        {
            if (BindingContext is SkilledSaveViewModel)
            {
                SkilledItem skilledItem = ((SkilledSaveViewModel)BindingContext).SkilledItem;

                await _skilledService.UpdateSkilledItemAsync(skilledItem);

                await new NavigationService(null).NavigateToAsync<MainViewModel>();
            }
        }

        


    }
}