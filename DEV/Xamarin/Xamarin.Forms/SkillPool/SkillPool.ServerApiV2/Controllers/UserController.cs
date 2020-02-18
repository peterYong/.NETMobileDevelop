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
    public class UserController : ControllerBase
    {
        private UserService _userService;

        /// <summary>
        /// 此处需要注入服务[在startUP的服务配置里面注入]
        /// </summary>
        /// <param name="bannerService"></param>
        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/User?email=
        [HttpGet]
        public ResponseModel Get(string email)
        {
            //return "value";
            ResponseModel responseModel = _userService.GetUser(email);
            return responseModel;
        }

       

        // POST: api/User
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/User/5
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
