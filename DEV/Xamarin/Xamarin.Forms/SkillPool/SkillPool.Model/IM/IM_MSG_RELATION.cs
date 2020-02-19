using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    /// <summary>
    /// 消息索引表
    /// </summary>
    public class IM_MSG_RELATION
    {
        public int ID { get; set; }

        public int OwnerUID { get; set; }
        public int OtherUID { get; set; }

        public int MID { get; set; }

        /// <summary>
        /// 发件：0， 收件：1
        /// </summary>
        public int Type { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
