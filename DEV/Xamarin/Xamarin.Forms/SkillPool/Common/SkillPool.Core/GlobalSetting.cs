using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Core
{
    public class GlobalSetting
    {
        public string AuthToken { get; set; }
        public GlobalSetting()
        {
            AuthToken = "INSERT AUTHENTICATION TOKEN";

        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();



       
    }
}
