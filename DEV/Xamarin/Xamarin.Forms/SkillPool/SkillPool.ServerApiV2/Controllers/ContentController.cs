using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillPool.Common.Extensions;
using SkillPool.Model.IM;
using SkillPool.Server.IM;

namespace SkillPool.ServerApiV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private ContentService _contentService;

        /// <summary>
        /// 此处需要注入服务[在startUP的服务配置里面注入]
        /// </summary>
        /// <param name="bannerService"></param>
        public ContentController(ContentService contentService)
        {
            _contentService = contentService;
        }

        // GET: api/Content?senderID= &recipientID=
        [HttpGet]
        public ResponseModel Get(int senderID, int recipientID)
        {
            //IM_MSG_CONTENT request = new IM_MSG_CONTENT()
            //{
            //    Content = "测试post",
            //    SenderID = 1004,
            //    RecipientID = 1005,
            //    MsgType = 0,
            //    CreateTime = DateTime.Now
            //};
            //string adgs = request.ToJSON();

            ResponseModel responseModel = _contentService.GetContentList(senderID, recipientID);
            return responseModel;
        }


        // POST api/content 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        //public ResponseModel Post([FromBody] string value)  //在postman中基于json字符串 测试时用这种
        public ResponseModel Post([FromBody] IM_MSG_CONTENT value)  //代码中通过HttpClient.PostAsync(url,HttpContent)时用这种
        {
            //IM_MSG_CONTENT iM_MSG_CONTENT = Newtonsoft.Json.JsonConvert.DeserializeObject<IM_MSG_CONTENT>(value);
            //return _contentService.AddMsgContent(iM_MSG_CONTENT);

            return _contentService.AddMsgContent(value);
        }
    }
}