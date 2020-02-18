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
        /// 获取Banner集合
        /// </summary>
        //public ResponseModel GetBannerList()
        //{
        //    var users = _db.USERs.ToList();
        //    var response = new ResponseModel();

        //    response.Code = 200;
        //    response.Result = "Banner集合获取成功";
        //    response.Data = new List<BannerModel>();
        //    foreach (var banner in banners)
        //    {
        //        response.Data.Add(new BannerModel
        //        {
        //            Id = banner.Id,
        //            Image = banner.Image,
        //            Url = banner.Url,
        //            Remark = banner.Remark
        //        });
        //    }
        //    return response;
        //}
    }
}
