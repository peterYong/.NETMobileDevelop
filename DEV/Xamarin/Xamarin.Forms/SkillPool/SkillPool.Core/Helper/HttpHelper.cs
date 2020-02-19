using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;

namespace SkillPool.Core.Helper
{
    /// <summary>
    /// 向WebApi请求数据
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// post请求数据
        /// </summary>
        /// <param name="jsonContent"></param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<HttpResponse> Send(string jsonContent, string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true;
            HttpResponse response = new HttpResponse() { ErrorCode = 0 };
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json"),
            };
            using (HttpClient client = GetHttpClient(url, 100))
            {
                try
                {
                    var httpResponseMessage = await client.SendAsync(request);
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        response.ErrorCode = 500;
                        response.Message = "http請求失敗";
                        return response;
                    }
                    else
                    {
                        var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        response.Data = result;
                        return response;
                    }
                }
                catch (AggregateException ae)
                {
                    response.Message = ae.ToString();
                    response.ErrorCode = 500;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Message = ex.ToString();
                    response.ErrorCode = 500;
                    return response;
                }
            }
        }
        private static HttpClient GetHttpClient(string url, int timeout)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            var client = new HttpClient()
            {
                BaseAddress = new Uri(url),
                Timeout = TimeSpan.FromSeconds(timeout)
            };
            return client;
        }


        /// <summary>
        /// get请求数据
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<HttpResponse> Get(string url)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Ssl3;
            ServicePointManager.ServerCertificateValidationCallback += (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) => true;

            HttpResponse response = new HttpResponse() { ErrorCode = 0 };
            using (HttpClient client = GetHttpClient(url, 100))
            {
                try
                {
                    var httpResponseMessage = await client.GetAsync(url);
                    if (!httpResponseMessage.IsSuccessStatusCode)
                    {
                        var xmlResult = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        response.ErrorCode = 500;
                        response.Message = "http請求失敗";
                        return response;
                    }
                    else
                    {
                        var result = httpResponseMessage.Content.ReadAsStringAsync().Result;
                        response.Data = result;
                        return response;
                    }
                }
                catch (AggregateException ae)
                {
                    response.Message = ae.ToString();
                    response.ErrorCode = 500;
                    return response;
                }
                catch (Exception ex)
                {
                    response.Message = ex.ToString();
                    response.ErrorCode = 500;
                    return response;
                }
            }
        }

    }

    /// <summary>
    /// http请求返回体
    /// </summary>
    public class HttpResponse
    {
        /// <summary>
        /// 错误代码，0成功，其他失败
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// 调http接口产生的message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 接口返回的数据
        /// </summary>
        public string Data { get; set; }
    }
}
