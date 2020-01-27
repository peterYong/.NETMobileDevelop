using SkillPool.Core.ViewModels;
using SkillPool.Core.ViewModels.Base;
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
    public partial class MainView : TabbedPage
    {
        public MainView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            MessagingCenter.Subscribe<MainViewModel, int>(this, message: MessageKeys.ChangeTab, (sender, arg) =>
               {
                   switch (arg)
                   {
                       case 0:
                           CurrentPage = SkilledView;
                           break;
                       case 1:
                           CurrentPage = ReadyLearnView;
                           break;
                       case 2:
                           CurrentPage = FindingView;
                           break;
                       case 3:
                           CurrentPage = ProfileView;
                           break;
                   }
               });

            await ((SkilledViewModel)SkilledView.BindingContext).InitializeAsync(null);
            await ((ReadyLearnViewModel)ReadyLearnView.BindingContext).InitializeAsync(null);
            await ((FindingViewModel)FindingView.BindingContext).InitializeAsync(null);
            await ((ProfileViewModel)ProfileView.BindingContext).InitializeAsync(null);
        }

        protected async override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            if (CurrentPage is SkilledView)
            {
                await (SkilledView.BindingContext as SkilledViewModel).InitializeAsync(null);
            }
            else if (CurrentPage is ReadyLearnView)
            {
                await (ReadyLearnView.BindingContext as ViewModelBase).InitializeAsync(null);
            }
            else if (CurrentPage is FindingView)
            {
                await (FindingView.BindingContext as ViewModelBase).InitializeAsync(null);
            }
            else if (CurrentPage is ProfileView)
            {
                await (ProfileView.BindingContext as ViewModelBase).InitializeAsync(null);
            }
        }
    }
}