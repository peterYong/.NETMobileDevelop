using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    /// <summary>
    /// 某人的联系人列表中的每人的最近一条消息
    /// </summary>
   public class MsgContact
    {
        public int OwnerUID { get; set; }
        public int OtherUID { get; set; }

        public int MID { get; set; }

        /// <summary>
        /// 0:发件箱，1:收件箱
        /// </summary>
        public int Type { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 消息类型：0:文本消息，1:语音信息
        /// </summary>
        public int MsgType { get; set; }
    }
}
