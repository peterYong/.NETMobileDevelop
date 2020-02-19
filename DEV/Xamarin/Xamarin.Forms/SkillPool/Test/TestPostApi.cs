using Newtonsoft.Json;
using SkillPool.Core.Helper;
using SkillPool.Core.ViewModels.IM;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    public class TestPostApi
    {
        public static void SendMsgCommandAsync()
        {
            string msg = "huaisdgsadg";
            if (string.IsNullOrEmpty(msg))
            {
                return;
            }

            //var url = "http://120.79.67.39/api/content";
            var url = "http://localhost:5000/api/content";
            IM_MSG_CONTENT request = new IM_MSG_CONTENT()
            {
                MID=152,
                Content = msg,
                SenderID = 1010,
                RecipientID = 1011,
                MsgType = 0,
                CreateTime = DateTime.Now
            };

            string response = WebApiHelper.SendApi(JsonConvert.SerializeObject(request), url);
            //string response = WebApiHelper.SendApi(request, url);
            var temp = Newtonsoft.Json.JsonConvert.DeserializeObject<ResponseModel>(response);
            if (temp?.Code == 200)
            {
                //聊天信息添加成功

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
