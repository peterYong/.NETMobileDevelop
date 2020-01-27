using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace SkillPool.Core.Validations
{
    /// <summary>
    /// 密码规则
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PassWordRule<T> : IValidationRule<T>
    {
        public string ValidationMessage
        {
            get ;
            set ;
        }

        /// <summary>
        /// 密码最少8位，必须同时包含数字和字母
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            string rule = @"^(?![0-9]+$)(?![a-zA-Z]+$)[0-9A-Za-z]{8,}$";
            bool vilid = Regex.IsMatch(str, rule);

            return vilid;
        }
    }
}
