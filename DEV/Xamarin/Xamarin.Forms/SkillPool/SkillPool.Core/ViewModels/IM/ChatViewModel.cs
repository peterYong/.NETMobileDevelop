using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SkillPool.Common.Extensions;
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
    public class ChatViewModel : ViewModelBase
    {
        #region  Field
        private string _username;
        private ObservableCollection<ContentModel> _contents;
        private string _msg;
        /// <summary>
        /// 接收者
        /// </summary>
        private IM_USER recipient = new IM_USER();
        #endregion


        #region BindableProperties
        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 历史聊天信息
        /// </summary>
        public ObservableCollection<ContentModel> Contents
        {
            get { return _contents; }
            set
            {
                _contents = value;
                OnPropertyChanged();
            }
        }
        /// <summary>
        /// 聊天信息
        /// </summary>
        public string Msg
        {
            get { return _msg; }
            set { _msg = value; OnPropertyChanged(); }
        }

        #endregion


        public override async Task InitializeAsync(object navigationData)
        {
            //传递的是联系人
            if (navigationData is IM_USER)
            {
                var item = navigationData as IM_USER;
                //SkilledItem = await _skilledService.GetSkilledItemByIdAsync(item.Id).ConfigureAwait(false);
                UserName = item.UserName;
                recipient = item;
                InitData(GlobalSetting.Instance.IM_USER, item);
            }
        }

        private void InitData(IM_USER sender, IM_USER recipient)
        {
            Msg = "";
            Contents = new ObservableCollection<ContentModel>();
            List<IM_MSG_CONTENT> temp = GetContent(sender.UID, recipient.UID);
            foreach (var item in temp)
            {
                Contents.Add(new ContentModel
                {
                    Avatar = sender.UID == item.SenderID ? sender.Avatar : recipient.Avatar,
                    Content = item.Content,
                    CreateTime = item.CreateTime,
                    MID = item.MID,
                    MsgType = item.MsgType,
                    RecipientID = item.RecipientID,
                    SenderID = item.SenderID
                });
            }
        }

        /// <summary>
        /// 获取聊天信息
        /// </summary>
        /// <param name="senderID"></param>
        /// <param name="recipientID"></param>
        private List<IM_MSG_CONTENT> GetContent(int senderID, int recipientID)
        {
            List<IM_MSG_CONTENT> contents = new List<IM_MSG_CONTENT>();
            string url = "http://120.79.67.39/api/content?senderID=" + senderID + "&recipientID=" + recipientID;
            string response = WebApiHelper.InvokeApi(url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp.Code == 200)
            {
                Newtonsoft.Json.Linq.JArray jObject = temp.Data;
                contents = jObject.ToObject<List<IM_MSG_CONTENT>>();
            }
            return contents;
        }


        public ICommand SendMsgCommand => new Command(async () => await SendMsgCommandAsync());


        /// <summary>
        /// 发送聊天信息
        /// </summary>
        /// <returns></returns>
        private async Task SendMsgCommandAsync()
        {
            string msg = Msg;
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            var url = "http://120.79.67.39/api/content";
            IM_MSG_CONTENT request = new IM_MSG_CONTENT()
            {
                Content = msg,
                SenderID = GlobalSetting.Instance.IM_USER.UID,
                RecipientID = recipient.UID,
                MsgType = 0,
                CreateTime = DateTime.Now
            };

            string response = WebApiHelper.SendApi(JsonConvert.SerializeObject(request), url);
            //string response = WebApiHelper.SendApi(request, url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp?.Code == 200)
            {
                //聊天信息添加成功
                Contents.Add(new ContentModel()
                {
                    Avatar = GlobalSetting.Instance.IM_USER.Avatar,
                    Content = msg,
                    CreateTime = request.CreateTime,
                    MID = request.MID,
                    MsgType = request.MsgType,
                    RecipientID = request.RecipientID,
                    SenderID = request.SenderID
                });
            }


            //#region HttpHelper.Send
            //var result = HttpHelper.Send(JsonConvert.SerializeObject(request), url).Result;
            //if (result.ErrorCode == 0)
            //{
            //    if (result.Data != null)
            //    {
            //        //根本不用定义返回Model
            //        //OrganizationOfflineResponse rs = JsonConvert.DeserializeObject<OrganizationOfflineResponse>(result.Data);

            //        //采用动态解析，只获取所需要的参数【注意定义为可null类型】
            //        //dynamic djson = JToken.Parse(result.Data) as dynamic;
            //        //if (djson.head.code == 0)
            //        //{
            //        //    dynamic json = djson.data;
            //        //    string name = json.OrganizationID;
            //        //    string company = json.OrganizationCode;
            //        //    DateTime? entered = json.Entered;
            //        //    int? type = json.Type;
            //        //    Console.WriteLine("name:" + name);
            //        //    Console.WriteLine("company:" + company);
            //        //    Console.WriteLine("type:" + type);

            //        //    Console.WriteLine("entered:" + entered);
            //        //}

            //        //Newtonsoft.Json.Linq.JArray jObject = result.Data;
            //        //GlobalSetting.Instance.IM_Contants = jObject.ToObject<List<IM_USER>>();
            //        ResponseModel responseModel = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(result.Data);
            //        if (responseModel.Code == 0)
            //        {

            //            Contents.Add(new ContentModel()
            //            {
            //                Avatar = GlobalSetting.Instance.IM_USER.Avatar,
            //                Content = msg,
            //                CreateTime = request.CreateTime,
            //                MID = request.MID,
            //                MsgType = request.MsgType,
            //                RecipientID = request.RecipientID,
            //                SenderID = request.SenderID
            //            });
            //        }
            //    }
            //}
            //#endregion

        }
    }

}
