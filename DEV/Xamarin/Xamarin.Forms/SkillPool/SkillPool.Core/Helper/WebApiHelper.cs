using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SkillPool.Core.Helper
{
    public static class WebApiHelper
    {

        /// <summary>
        /// 调用api，获取数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string InvokeApi(string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                HttpRequestMessage httpRequest = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri(url)
                };
                var result = httpClient.SendAsync(httpRequest).Result;
                string content = result.Content.ReadAsStringAsync().Result;
                return content;
            }
        }

        public static string SendApi(string jsonContent, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //表头参数
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //转为链接需要的格式
                HttpContent httpContent = new JsonContent(jsonContent);
                //请求
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }
        public static string SendApi(object jsonContent, string url)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                //表头参数
                httpClient.DefaultRequestHeaders.Accept.Clear();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //转为链接需要的格式
                HttpContent httpContent = new JsonContent(jsonContent);
                //请求
                HttpResponseMessage response = httpClient.PostAsync(url, httpContent).Result;

                //HttpRequestMessage httpRequest = new HttpRequestMessage()
                //{
                //    Method = HttpMethod.Post,
                //    RequestUri = new Uri(url),
                //    Content=new StringContent(jsonContent, Encoding.UTF8, "application/json");
                //};
                //var result = httpClient.PostAsync(httpRequest).Result;
                string content = response.Content.ReadAsStringAsync().Result;
                return content;
            }
        }


    }

    public class JsonContent : StringContent
    {
        public JsonContent(string str) :
           base(str, Encoding.UTF8, "application/json")
        { }

        public JsonContent(object obj) :
            base(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json")
        { }
    }

}
