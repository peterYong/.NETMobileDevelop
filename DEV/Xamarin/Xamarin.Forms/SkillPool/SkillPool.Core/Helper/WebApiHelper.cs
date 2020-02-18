using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace SkillPool.Core.Helper
{
    public static class WebApiHelper
    {
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
    }
}
