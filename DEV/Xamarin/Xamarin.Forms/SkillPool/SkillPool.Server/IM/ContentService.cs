using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillPool.Server.IM
{
    public class ContentService
    {
        private Db _db;
        public ContentService(Db db)
        {
            this._db = db;
        }

        /// <summary>
        /// 获取聊天信息集合
        /// </summary>
        /// <param name="senderID">发送者</param>
        /// <param name="recipientID">接收者</param>
        /// <returns></returns>
        public ResponseModel GetContentList(int senderID, int recipientID)
        {
            var response = new ResponseModel()
            {
                Code = 0,
                Result = "获取数据失败"
            };
            var contents = _db.IM_MSG_CONTENT.Where(c => (c.RecipientID == recipientID && c.SenderID == senderID) || (c.RecipientID == senderID && c.SenderID == recipientID)).OrderBy(d => d.CreateTime);
            if (contents != null && contents.Count() > 0)
            {
                response.Code = 200;
                response.Result = "聊天信息获取成功";
                response.Data = contents.ToList();
            }
            return response;
        }

    }
}
