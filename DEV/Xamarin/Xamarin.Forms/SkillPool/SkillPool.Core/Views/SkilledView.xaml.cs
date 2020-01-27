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
    public partial class SkilledView : ContentPage
    {
        private readonly ISkilledService _skilledService;
        public SkilledView()
        {
            InitializeComponent();

            _skilledService = new SkilledSqliteMockService();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnDeleteSkilledClicked(object sender, EventArgs e)
        {
            var menuItem = (MenuItem)sender;
            var skilledItem = (SkilledItem)menuItem.CommandParameter;

            bool isDelete = await DisplayAlert("删除Skill", $"你确定要删除'{skilledItem.Name}'吗？", "确定", "取消");
            if (isDelete)
            {
                await _skilledService.RemoveSkilledItemAsync(skilledItem).ConfigureAwait(false);
                await ((SkilledViewModel)BindingContext).InitializeAsync(null).ConfigureAwait(false);
                
            }
        }

    }
}