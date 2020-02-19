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

        /// <summary>
        /// 增加聊天信息
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public ResponseModel AddMsgContent(IM_MSG_CONTENT content)
        {
            var response = new ResponseModel()
            {
                Code = 0,
                Result = "添加数据失败"
            };
            //增加聊天信息
            //1、内容表增加一条数据，2、索引表增加两条数据，3、最近联系人增加/更新一条记录
            _db.IM_MSG_CONTENT.Add(content);
            int i = _db.SaveChanges();
            if (i > 0)
            {

                //添加索引
                IM_MSG_RELATION r1 = new IM_MSG_RELATION()
                {
                    MID = content.MID,
                    CreateTime = content.CreateTime,
                    OwnerUID = content.SenderID,
                    OtherUID = content.RecipientID,
                    Type = (int)SendReceiveType.Send,
                };
                IM_MSG_RELATION r2 = r1;
                r2.Type = (int)SendReceiveType.Receive;
                r2.OwnerUID = content.RecipientID;
                r2.OtherUID = content.SenderID;
                _db.IM_MSG_RELATION.Add(r1);
                _db.IM_MSG_RELATION.Add(r2);
                int j = _db.SaveChanges(true);

                //添加最近联系人

                IM_MSG_CONTACT temp = _db.IM_MSG_CONTACT.FirstOrDefault(c => c.OwnerUID == content.SenderID && c.OtherUID == content.RecipientID);
                int k = 0;
                if (temp != null)
                {
                    //更新
                    temp.MID = content.MID;
                    temp.CreateTime = content.CreateTime;
                    _db.IM_MSG_CONTACT.Update(temp);
                    k = _db.SaveChanges();
                }
                else
                {
                    //增加
                    IM_MSG_CONTACT c1 = new IM_MSG_CONTACT()
                    {
                        MID = content.MID,
                        CreateTime = content.CreateTime,
                        OwnerUID = content.SenderID,
                        OtherUID = content.RecipientID,
                        Type = (int)SendReceiveType.Send
                    };
                    _db.IM_MSG_CONTACT.Add(c1);
                    k = _db.SaveChanges();
                }
                if (j > 0 && k > 0)
                {
                    response.Code = 200;
                    response.Result = "聊天信息添加成功";
                }
            }
            return response;
        }

    }
}
