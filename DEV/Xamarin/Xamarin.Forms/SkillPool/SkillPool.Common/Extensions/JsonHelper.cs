using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SkillPool.Common.Extensions
{
    /// <summary>
    /// JSON序列化，反序列化
    /// </summary>
    public static class JsonHelper
    {
        /// <summary>
        /// 使用json序列化为字符串,对象为null会直接输出"null"
        /// </summary>
        /// <param name="input">输入对象</param>
        /// <param name="dateTimeFormat">默认null,即使用json.net默认的序列化机制，如："\/Date(1439335800000+0800)\/"</param>
        /// <returns></returns>
        public static string ToJSON(this object input, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            var settings = new JsonSerializerSettings()
            {
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
            };

            settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            settings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;

            if (!string.IsNullOrWhiteSpace(dateTimeFormat))
            {
                var jsonConverter = new List<JsonConverter>()
                {
                    new Newtonsoft.Json.Converters.IsoDateTimeConverter(){ DateTimeFormat = dateTimeFormat }//如： "yyyy-MM-dd HH:mm:ss"
                };
                settings.Converters = jsonConverter;
            }
            var format = Newtonsoft.Json.Formatting.Indented;
            var json = JsonConvert.SerializeObject(input, format, settings);
            return json;
        }

        /// <summary>
        /// 从序列化字符串里反序列化，input可为null
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="input"></param>
        /// <param name="dateTimeFormat">默认null,即使用json.net默认的序列化机制</param>
        /// <returns></returns>
        public static T FromJSON<T>(this string input, string dateTimeFormat = "yyyy-MM-dd HH:mm:ss")
        {
            if (input == null)  //不加这行的话，input==null时会报错
            {
                return JsonConvert.DeserializeObject<T>("");
            }
            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            };

            if (!string.IsNullOrWhiteSpace(dateTimeFormat))
            {
                var jsonConverter = new List<JsonConverter>()
                {
                    new Newtonsoft.Json.Converters.IsoDateTimeConverter(){ DateTimeFormat = dateTimeFormat }//如： "yyyy-MM-dd HH:mm:ss"
                };
                settings.Converters = jsonConverter;
            }

            return JsonConvert.DeserializeObject<T>(input, settings);
        }
    }
}
