using SkillPool.Core.ViewModels.Base;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.ViewModels.IM
{
    public class ChatViewModel : ViewModelBase
    {

        private string _username;
        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        public override async Task InitializeAsync(object navigationData)
        {
            if (navigationData is IM_USER)
            {
                var item = navigationData as IM_USER;
                //SkilledItem = await _skilledService.GetSkilledItemByIdAsync(item.Id).ConfigureAwait(false);
                UserName = item.UserName;
            }
        }
    }
}
