﻿using SkillPool.Common.Extensions;
using SkillPool.Core.Helper;
using SkillPool.Core.ViewModels.Base;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SkillPool.Core.ViewModels.IM
{
    public class ContactViewModel : ViewModelBase
    {
        public ContactViewModel()
        {
            InitData();
        }


        private ObservableCollection<IM_USER> _users;
        public ObservableCollection<IM_USER> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                OnPropertyChanged();
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

        private void GetContatct(int uid)
        {
            string url = "http://120.79.67.39/api/contact/" + uid;
            string response = WebApiHelper.InvokeApi(url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp.Code == 200)
            {
                Newtonsoft.Json.Linq.JArray jObject = temp.Data;
                GlobalSetting.Instance.IM_Contants = jObject.ToObject<List<IM_USER>> ();
            }
        }

        public ICommand ContactCommand => new Command<IM_USER>(async (user) => await ContactCommandAsync(user));

        private async Task ContactCommandAsync(IM_USER user)
        {
            //SkilledItem skilled = (SkilledItem)BindingContext;
            await NavigationService.NavigateToAsync<ChatViewModel>(user).ConfigureAwait(false);
        }



    }
}
