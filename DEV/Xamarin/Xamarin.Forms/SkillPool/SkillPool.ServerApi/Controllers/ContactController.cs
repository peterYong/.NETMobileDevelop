using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SkillPool.Model.IM;
using SkillPool.Server.IM;

namespace SkillPool.ServerApi.Controllers
{
    /// <summary>
    /// 联系人
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {

        private UserService _userService;

        /// <summary>
        /// 此处需要注入服务[在startUP的服务配置里面注入]
        /// </summary>
        /// <param name="bannerService"></param>
        public ContactController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/Contact
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }


        // GET: api/Contact/5
        [HttpGet("{id}")]
        public ResponseModel Get(int id)
        {
            //return "value";
            ResponseModel responseModel = _userService.GetContactList(id);
            return responseModel;
        }

        // POST: api/Contact
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Contact/5
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
