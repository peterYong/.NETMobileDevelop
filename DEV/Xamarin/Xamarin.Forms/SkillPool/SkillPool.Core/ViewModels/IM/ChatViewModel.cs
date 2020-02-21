using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RedisHelper;
using SkillPool.Common.Extensions;
using SkillPool.Core.Helper;
using SkillPool.Core.ViewModels.Base;
using SkillPool.Core.Views;
using SkillPool.Core.Views.IM;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
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

        /// <summary>
        /// 当前页面
        /// </summary>
        private ChatView currentPage;
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

        #region Init

        public ChatViewModel()
        {
            if (!GlobalSetting.Instance.HasSubRedis)
            {
                GlobalSetting.Instance.HasSubRedis = true;
                RedisCacheHelper.Init();
                Task.Run(() =>
                {
                    RedisCacheHelper.RedisSub("IM", GetMessage);
                });


                Task.Run(() =>
                {
                    Timer timer = new Timer();
                    timer.Interval = 5000;
                    timer.Elapsed += Timer_Elapsed;
                    timer.Start();
                });
            }
        }

        /// <summary>
        /// 后台定时到redis中取聊天信息，并且通过WebApi服务添加到数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            IDictionary<string, IM_MSG_CONTENT> dic = new Dictionary<string, IM_MSG_CONTENT>();
            dic = RedisCacheHelper.GetAll<IM_MSG_CONTENT>("IM:*");
            if (dic != null)
            {
                foreach (var item in dic)
                {
                    //Console.WriteLine($"key={item.Key},value={item.Value}");
                    if (SendApi(item.Value))
                    {
                        RedisCacheHelper.Remove(item.Key);
                    }
                }
            }

        }

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
                    SenderID = item.SenderID,
                    IsSelf = sender.UID == item.SenderID
                });
            }

            //直接显示最后一条
            CustomNavigationView mainPage = Application.Current.MainPage as CustomNavigationView;

            if (mainPage != null && Contents.Count >= 1)
            {
                 currentPage = mainPage.Navigation.NavigationStack[mainPage.Navigation.NavigationStack.Count - 1] as ChatView;
                //Task.Delay(100).ContinueWith(t =>
                // {

                // });

                Device.BeginInvokeOnMainThread(() =>
                {
                    currentPage?.chatListView.ScrollTo(Contents[Contents.Count - 1], ScrollToPosition.End, false);
                });

            }

        }

        /// <summary>
        /// redis订阅，有人发信息过来就会收到
        /// </summary>
        /// <param name="channel"></param>
        /// <param name="message"></param>
        private void GetMessage(string channel, string message)
        {
            IM_MSG_CONTENT item = JsonConvert.DeserializeObject<IM_MSG_CONTENT>(message);
            if (item != null && item.SenderID == recipient.UID && item.RecipientID == GlobalSetting.Instance.IM_USER.UID)  //是当前联系人发给我的信息
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    Contents.Add(new ContentModel
                    {
                        Avatar = recipient.Avatar,
                        Content = item.Content,
                        CreateTime = item.CreateTime,
                        MID = item.MID,
                        MsgType = item.MsgType,
                        RecipientID = item.RecipientID,
                        SenderID = item.SenderID
                    });
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

        #endregion

        #region Command

        public ICommand SendMsgCommand => new Command(async () => await SendMsgCommandAsync());

        /// <summary>
        /// 发送聊天信息
        /// </summary>
        /// <returns></returns>
        private async Task SendMsgCommandAsyncOne()
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
            PubRedis(request);

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

        }

        /// <summary>
        /// 发布消息到redis通道IM，，，并且增加到reids。立马返回，存库操作在后台执行
        /// </summary>
        /// <param name="message"></param>
        private long PubRedis(IM_MSG_CONTENT message)
        {
            string channel = "IM";
            long result = RedisCacheHelper.RedisPub(channel, Newtonsoft.Json.JsonConvert.SerializeObject(message));
            RedisCacheHelper.Add($"IM:{message.SenderID}-{message.RecipientID}-{DateTime.Now.ToString("yyyyMMddHHmmss")}", message, DateTime.Now.AddMonths(1));
            return result;
        }

        private async Task SendMsgCommandAsync()
        {
            string msg = Msg;
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            IM_MSG_CONTENT request = new IM_MSG_CONTENT()
            {
                Content = msg,
                SenderID = GlobalSetting.Instance.IM_USER.UID,
                RecipientID = recipient.UID,
                MsgType = 0,
                CreateTime = DateTime.Now
            };
            long re = PubRedis(request);
            if (re >= 0)
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
                    SenderID = request.SenderID,
                    IsSelf = true
                });
                //清空聊天信息框，同事拉到最后一条信息
                Msg = "";
                Device.BeginInvokeOnMainThread(() =>
                {
                    currentPage?.chatListView.ScrollTo(Contents[Contents.Count - 1], ScrollToPosition.End, false);
                });
            }

        }

        /// <summary>
        /// 发送信息 通过api服务，保存到数据库
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private bool SendApi(IM_MSG_CONTENT request)
        {
            bool result = false;
            var url = "http://120.79.67.39/api/content";
            string response = WebApiHelper.SendApi(JsonConvert.SerializeObject(request), url);
            //string response = WebApiHelper.SendApi(request, url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp?.Code == 200)
            {
                result = true;
            }
            return result;
        }
        #endregion

    }

}
