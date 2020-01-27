using SkillPool.Core.ViewModels.Base;
using SkillPool.Model.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels
{
    public class ProfileViewModel : ViewModelBase
    {

        #region Command

        public ICommand LogoutCommand => new Command(async () => await LogoutAsync().ConfigureAwait(false));
        private async Task LogoutAsync()
        {
            IsBusy = true;

            // Logout
            await NavigationService.NavigateToAsync<LoginViewModel>(new LogoutParameter { Logout = true });
            await NavigationService.RemoveBackStackAsync();

            IsBusy = false;
        }
        #endregion
    }
}
