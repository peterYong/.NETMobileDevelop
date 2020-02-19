using SkillPool.Core.Helper;
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

        private void InitData()
        {
            Users = new ObservableCollection<IM_USER>();
            IM_USER currentUser = GlobalSetting.Instance.IM_USER;
            if (currentUser != null)
            {
                GetContatct(currentUser.UID);
            }
            Users = GlobalSetting.Instance.IM_Contants.ToObservableCollection();
        }


        private void GetContent(int ownerUID,int otherUID)
        {
            string url = "http://120.79.67.39/api/contact/" + uid;
            string response = WebApiHelper.InvokeApi(url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp.Code == 200)
            {
                Newtonsoft.Json.Linq.JArray jObject = temp.Data;
                GlobalSetting.Instance.IM_Contants = jObject.ToObject<List<IM_USER>>();
            }
        }

    }
}
