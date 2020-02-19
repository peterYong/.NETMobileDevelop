using Microsoft.EntityFrameworkCore;
using SkillPool.Model.IM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SkillPool.Server.IM
{
    public class UserService
    {
        private Db _db;
        public UserService(Db db)
        {
            this._db = db;
        }

        /// <summary>
        /// 获取账户
        /// </summary>
        public ResponseModel GetUser(string email)
        {
            var response = new ResponseModel()
            {
                Code = 0,
                Result = "获取数据失败"
            };
            var user = _db.IM_USER.FirstOrDefault(c => c.Email == email);
            if (user != null)
            {
                response.Code = 200;
                response.Result = "联系人集合获取成功";
                response.Data = new IM_USER
                {
                    UID = user.UID,
                    UserName = user.UserName,
                    Email = user.Email,
                    Avatar = user.Avatar
                };
            }
            return response;
        }

        /// <summary>
        /// 获取联系人集合
        /// </summary>
        public ResponseModel GetContactList(int uid)
        {
            var response = new ResponseModel()
            {
                Code = 0,
                Result = "获取数据失败"
            };
            var contacts = _db.IM_CONTACT.Where(c => c.UID == uid);
            if (contacts != null && contacts.Count() > 0)
            {
                response.Code = 200;
                response.Result = "联系人集合获取成功";
                response.Data = new List<IM_USER>();
                foreach (var user in contacts)
                {
                    var temp = _db.IM_USER.FirstOrDefault(c => c.UID == user.OtherUID);
                    response.Data.Add(new IM_USER
                    {
                        UID = user.OtherUID,
                        UserName = temp.UserName,
                        Email = temp.Email,
                        Avatar = temp.Avatar
                    });
                }
            }
            return response;
        }


        /// <summary>
        /// 获取某人的最近的联系人集合（包括最近一条内容用于展示）
        /// </summary>
        public ResponseModel GetMsgContactList(int uid)
        {
            var response = new ResponseModel()
            {
                Code = 0,
                Result = "获取数据失败"
            };
            var contacts = _db.IM_MSG_CONTACT.Where(c => c.OwnerUID == uid);
            if (contacts != null && contacts.Count() > 0)
            {
                response.Code = 200;
                response.Result = "联系人集合获取成功";
                response.Data = new List<MsgContact>();
                foreach (var item in contacts)
                {
                    var temp = _db.IM_MSG_CONTENT.FirstOrDefault(c => c.MID == item.MID);
                    response.Data.Add(new MsgContact
                    {
                        OwnerUID = item.OwnerUID,
                        OtherUID = item.OtherUID,
                        Type = item.Type,
                        MID = item.MID,
                        CreateTime = item.CreateTime,
                        Content = temp?.Content,
                        MsgType = temp == null ? 0 : temp.MsgType
                    });
                }
            }
            return response;
        }

    }
}
