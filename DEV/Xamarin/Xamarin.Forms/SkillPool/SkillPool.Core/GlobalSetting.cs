using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core
{
    public class GlobalSetting
    {
        public string AuthToken { get; set; }
        /// <summary>
        /// 账户
        /// </summary>
        public IM_USER IM_USER { get; set; }
        /// <summary>
        /// 联系人列表
        /// </summary>
        public List<IM_USER> IM_Contants { get; set; }

        /// <summary>
        /// 是否订阅了Redis
        /// </summary>
        public bool HasSubRedis { get; set; } = false;

        public GlobalSetting()
        {
            //AuthToken = "INSERT AUTHENTICATION TOKEN";
            
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();



       
    }
}
