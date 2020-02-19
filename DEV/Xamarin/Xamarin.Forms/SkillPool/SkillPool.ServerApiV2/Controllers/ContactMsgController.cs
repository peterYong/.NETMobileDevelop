using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillPool.Model.IM;

namespace SkillPool.ServerApiV2.Controllers
{
    /// <summary>
    /// 最近联系人信息【不需要了】
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactMsgController : ControllerBase
    {
        // GET: api/ContactMsg
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/ContactMsg/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }


        ///// <summary>
        ///// 获取某用户的最近联系人列表及最近一条信息
        ///// </summary>
        ///// <param name="id"></param>
        ///// <returns></returns>
        //// GET: api/ContactMsg/5
        //[HttpGet("{id}", Name = "Get")]
        //public ResponseModel Get(int id)
        //{
        //    ResponseModel responseModel = _userService.GetContactList(id);
        //    return responseModel;
        //}

        // POST: api/ContactMsg
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/ContactMsg/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
