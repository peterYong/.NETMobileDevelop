using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public ResponseModel Get(int senderID,int recipientID)
        {
            ResponseModel responseModel = _contentService.GetContentList(senderID, recipientID);
            return responseModel;
        }
    }
}