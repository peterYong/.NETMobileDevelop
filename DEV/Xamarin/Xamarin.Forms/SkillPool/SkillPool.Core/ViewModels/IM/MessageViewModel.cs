using SkillPool.Common.Extensions;
using SkillPool.Core.Helper;
using SkillPool.Core.ViewModels.Base;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels.IM
{
    /// <summary>
    /// 聊天信息列表
    /// </summary>
    public class MessageViewModel : ViewModelBase
    {
        List<MsgContact> contactMsgList = new List<MsgContact>();


        private ObservableCollection<MsgContact> _contactMsgs;
        /// <summary>
        /// 联系人信息列表
        /// </summary>
        public ObservableCollection<MsgContact> ContactMsgs
        {
            get { return _contactMsgs; }
            set
            {
                _contactMsgs = value;
                OnPropertyChanged();
            }
        }

        public MessageViewModel()
        {
            InitData();
        }

        private void InitData()
        {
            ContactMsgs = new ObservableCollection<MsgContact>();
            IM_USER currentUser = GlobalSetting.Instance.IM_USER;
            if (currentUser != null)
            {
                ContactMsgs = GetContatctMsg(currentUser.UID);
            }
        }

        /// <summary>
        /// 获取联系人的最近一条信息
        /// </summary>
        /// <param name="uid"></param>
        private ObservableCollection<MsgContact> GetContatctMsg(int uid)
        {
            string url = "http://120.79.67.39/api/user?uid=" + uid;
            string response = WebApiHelper.InvokeApi(url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp.Code == 200)
            {
                Newtonsoft.Json.Linq.JArray jObject = temp.Data;
                contactMsgList = jObject.ToObject<List<MsgContact>>();
                return contactMsgList?.ToObservableCollection();
            }
            return null;
        }
        #region 
        public ICommand ContactCommand => new Command<MsgContact>(async (msgContact) => await ContactCommandAsync(msgContact));

        /// <summary>
        /// 单击联系人进入聊天界面
        /// </summary>
        /// <param name="msgContact"></param>
        /// <returns></returns>
        private async Task ContactCommandAsync(MsgContact msgContact)
        {
            IM_USER user = new IM_USER();
            if (GlobalSetting.Instance.IM_Contacts == null)
            {
                IMHelper.GetContatct(msgContact.OwnerUID);
            }
            if (GlobalSetting.Instance.IM_Contacts != null)
            {
                user = GlobalSetting.Instance.IM_Contacts.FirstOrDefault(u => u.UID == msgContact.OtherUID);
            }
            await NavigationService.NavigateToAsync<ChatViewModel>(user).ConfigureAwait(false);
        }


        #endregion


    }
}
