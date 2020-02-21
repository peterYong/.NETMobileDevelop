using SkillPool.Core.Helper;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core.ViewModels.IM
{
    public static class IMHelper
    {
        /// <summary>
        /// 获取联系人
        /// </summary>
        /// <param name="uid"></param>
        public static void GetContatct(int uid)
        {
            string url = "http://120.79.67.39/api/contact/" + uid;
            string response = WebApiHelper.InvokeApi(url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp.Code == 200)
            {
                Newtonsoft.Json.Linq.JArray jObject = temp.Data;
                GlobalSetting.Instance.IM_Contacts = jObject.ToObject<List<IM_USER>>();
            }
        }
    }
}
