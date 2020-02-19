using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    /// <summary>
    /// 消息内容表
    /// </summary>
    public class IM_MSG_CONTENT
    {
        public int ID { get; set; }

        public int MID { get; set; }
        public string Content { get; set; }

        public int SenderID { get; set; }

        public int RecipientID { get; set; }

        /// <summary>
        /// 消息类型：0:文本消息，1:语音信息
        /// </summary>
        public int MsgType { get; set; }
        
        public DateTime CreateTime { get; set; }
        
    }
}
