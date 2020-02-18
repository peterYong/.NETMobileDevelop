using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    /// <summary>
    /// 联系人
    /// </summary>
    public class IM_CONTACT
    {
        
        public int ID { get; set; }

        /// <summary>
        /// 归属人
        /// </summary>
        public int UID { get; set; }

        /// <summary>
        /// 关联人
        /// </summary>
        public int OtherUID { get; set; }

    }
}
