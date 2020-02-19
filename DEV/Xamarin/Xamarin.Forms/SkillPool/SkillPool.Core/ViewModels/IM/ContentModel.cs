using SkillPool.Model.IM;
using System;

namespace SkillPool.Core.ViewModels.IM
{
    /// <summary>
    /// 聊天内容实体
    /// </summary>
    public class ContentModel: IM_MSG_CONTENT
    {
        //public int MID { get; set; }
        //public string Content { get; set; }

        //public int SenderID { get; set; }

        //public int RecipientID { get; set; }

        ///// <summary>
        ///// 消息类型：0:文本消息，1:语音信息
        ///// </summary>
        //public int MsgType { get; set; }

        //public DateTime CreateTime { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
    }
}