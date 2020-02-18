using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Model.IM
{
    /// <summary>
    /// 返回结果 基础类
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// 0:失败;200:成功
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 查询结果说明
        /// </summary>
        public string Result { get; set; }

        /// <summary>
        /// 查询结果数据
        /// </summary>
        public dynamic Data { get; set; }
    }
}
