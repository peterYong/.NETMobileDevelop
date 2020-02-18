using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class IM_USER
    {
        
        public int ID { get; set; }
        public int UID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        
       
        
    }
}
