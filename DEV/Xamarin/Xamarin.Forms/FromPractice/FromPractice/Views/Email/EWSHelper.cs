using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Xml;

namespace FromPractice.Views.Email
{
    /// <summary>
    /// 使用托管类库去使用Exchange Webservice
    /// </summary>
    public class EWSHelper
    {
        /// <summary>
        /// ExchangeService对象
        /// </summary>
        public static ExchangeService service;

        private static EWSHelper instance;
        private static readonly object syncRoot = new object();

        public static EWSHelper GetInstance(EWSInitModel eWSInitModel)
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    //需要第二重判断，是因为两个线程同时过了第一重判断之后；
                    //一个等待，一个创建实例，然后等待的结束后 不能再创建实例（就通过 对象是否为null来判断）
                    if (instance == null)
                    {
                        instance = new EWSHelper(eWSInitModel);
                    }
                }
            }
            return instance;
        }
        private EWSHelper(EWSInitModel eWSInitModel)
        {
            //ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };//至关重要的一句 否则会报错：The Autodiscover service couldn't be located.

            //service = new ExchangeService(ExchangeVersion.Exchange2007_SP1);
            service = new ExchangeService();
            service.Credentials = new WebCredentials(eWSInitModel.Email, eWSInitModel.Password);
            var url = string.Format("https://{0}/ews/exchange.asmx", "mail.wotrus.com");
            service.Url = new Uri(url);

            service.TraceListener = new TraceListener();
            //service.TraceFlags = TraceFlags.All;
            service.TraceFlags = TraceFlags.EwsResponse | TraceFlags.EwsResponseHttpHeaders;
            service.TraceEnabled = true;
            //service.AutodiscoverUrl(_username, RedirectionUrlValidationCallback);  //这里mesince.com与wotrus.com不同，所以不用自动发现
        }
        private static bool RedirectionUrlValidationCallback(string redirectionUrl)
        {
            ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
            // The default for the validation callback is to reject the URL.
            bool result = false;
            Uri redirectionUri = new Uri(redirectionUrl);
            // Validate the contents of the redirection URL. In this simple validation
            // callback, the redirection URL is considered valid if it is using HTTPS
            // to encrypt the authentication credentials. 
            if (redirectionUri.Scheme == "https")
            {
                result = true;
            }
            return result;
        }

       

        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public SendResultEntity SendEmail(SendMailContent content)
        {
            //InitializeEWS();
            EmailMessage message = new EmailMessage(service);
            // 邮件主题
            message.Subject = content.Subject;
            message.Body = content.Body;
            message.ToRecipients.AddRange(content.ToList);
            if (content.CcList?.Count > 0)
                message.CcRecipients.AddRange(content.CcList);
            if (content.BccList?.Count > 0)
                message.BccRecipients.AddRange(content.CcList);
            //添加内嵌照片
            if (content.InnerPictures?.Count > 0)
            {
                int i = 0;
                foreach (var item in content.InnerPictures)
                {
                    message.Attachments.AddFileAttachment(item.Key, item.Value);
                    message.Attachments[i].IsInline = true;
                    //message.Attachments[i].ContentType=
                    message.Attachments[i].ContentId = item.Key;
                    i++;
                }
            }
            //添加附件
            //if (content.AttachmentFileNames?.Count > 0)
            //{
            //    foreach (var item in content.AttachmentFileNames)
            //    {
            //        message.Attachments.AddFileAttachment(item);
            //    }
            //}
            byte[] con1 = new byte[10 * 1024]; //10k
            byte[] con2 = new byte[4 * 1024 * 1024]; //50M  //附件大小有限制的，40M也大
            Random random = new Random();
            for (int i = 0; i < 1000; i++)
            {
                con2[i] = (byte)random.Next(200);
            }
            message.Attachments.AddFileAttachment("1.jpg", con1);
            message.Attachments.AddFileAttachment("2.zip", con2);
            SendResultEntity sendResultEntity = new SendResultEntity();
            try
            {
                if (content.IsSendSave)
                {
                    message.SendAndSaveCopy();
                }
                else
                {
                    message.Send();
                }
            }
            catch (Exception ex)
            {
                sendResultEntity.ResultInformation = $"邮件接收失败:{ex.Message}";
                sendResultEntity.ResultStatus = false;
            }
            return sendResultEntity;
        }

    }

    public class EWSInitModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Domian { get; set; }
    }

    class TraceListener : ITraceListener
    {
        #region ITraceListener Members

        /// <summary>
        /// 发送完成后 再触发的
        /// </summary>
        /// <param name="traceType"></param>
        /// <param name="traceMessage"></param>
        public void Trace(string traceType, string traceMessage)
        {
            CreateXMLTextFile(traceType, traceMessage.ToString());
        }
        #endregion
        private void CreateXMLTextFile(string fileName, string traceContent)
        {
            // Create a new XML file for the trace information.
            try
            {
                Debug.WriteLine(traceContent);
                // If the trace data is not valid XML, save it as a text document.
                System.IO.File.WriteAllText(fileName + ".txt", traceContent);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }

    /// <summary>
    /// 邮件发送结果
    /// </summary>
    public class SendResultEntity
    {
        /// <summary>
        /// 结果信息
        /// </summary>
        public string ResultInformation { get; set; } = "发送成功！";

        /// <summary>
        /// 结果状态
        /// </summary>
        public bool ResultStatus { get; set; } = true;
    }

    public class SendMailContent
    {
        /// <summary>
        /// 邮件主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 邮件主体
        /// </summary>
        public MessageBody Body { get; set; }
        /// <summary>
        /// 发送人列表
        /// </summary>
        public List<EmailAddress> ToList { get; set; }
        /// <summary>
        /// 抄送人列表
        /// </summary>
        public List<EmailAddress> CcList { get; set; }
        /// <summary>
        /// 密件抄送收件人列表
        /// </summary>
        public List<EmailAddress> BccList { get; set; }
        /// <summary>
        /// 发送后是否保存到已发送邮件列表
        /// </summary>
        public bool IsSendSave { get; set; }
        /// <summary>
        /// 附件文件路径列表
        /// </summary>
        public List<string> AttachmentFileNames { get; set; }

        /// <summary>
        /// 内嵌照片路径和其对应的CID名称
        /// </summary>
        public Dictionary<string, string> InnerPictures { get; set; }

    }
}
