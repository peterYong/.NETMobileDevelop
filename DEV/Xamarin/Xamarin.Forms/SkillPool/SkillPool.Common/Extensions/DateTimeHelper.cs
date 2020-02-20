using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SkillPool.Common.Extensions
{
    public static class DateTimeHelper
    {
        public static DateTime ToDTNow(this DateTime dateTime)
        {
            //string str = dateTime.ToString(new CultureInfo("zh-CN"));
            //return Convert.ToDateTime(str);
            return dateTime;
        }
    }
}
