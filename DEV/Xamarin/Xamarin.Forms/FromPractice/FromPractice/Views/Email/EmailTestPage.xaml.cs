using Microsoft.Exchange.WebServices.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FromPractice.Views.Email
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailTestPage : ContentPage
    {
        public EmailTestPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

            try
            {
                List<EmailAddress> emailAddresses = new List<EmailAddress>()
                {
                    new EmailAddress{Address="414281041@qq.com",Name="huy"},
                };

                string path = @"PKI.png";
                //string path1 = @"BaiduNetdisk_6.8.1.3.zip";
                SendMailContent content = new SendMailContent()
                {
                    Subject = "测试Exchange",
                    Body = new MessageBody($"平台：{Device.RuntimePlatform},now:{DateTime.Now.ToString()},This is the first email I've sent by using the EWS Managed API"),
                    ToList = new List<EmailAddress> {
                        new EmailAddress {
                            Address = "mes3@mesince.com",
                            Name = "mes3" }
                    },
                    //CcList = emailAddresses,
                    AttachmentFileNames = new List<string>() { path },
                };
                //EWSHelper eWSHelper = new EWSHelper("mes3@mesince.com", "hy147258369@", ""); //WOTRUS\mes3
                //EWSHelper eWSHelper = EWSHelper.Instance("mes3", "hy147258369@", "WOTRUS");
                EWSHelper eWSHelper = EWSHelper.GetInstance(new EWSInitModel() { Email = "mes3@mesince.com", Password = "hy147258369@", Domian = "WOTRUS" });

                if (EWSHelper.service.Url != null)
                {
                    Console.WriteLine($"应用程序通过服务进行身份验证，并发现了邮箱的EWS端点:{EWSHelper.service.Url}");
                }
                SendResultEntity res = eWSHelper.SendEmail(content);
                if (res != null && res.ResultStatus)
                {
                    string message = "发送成功";
                    error.Text = message;
                    Console.WriteLine(message);
                }
                else
                {
                    string message = res?.ResultInformation;
                    error.Text = message;
                    Console.WriteLine(message);
                }
            }
            catch (Exception ex)
            {
                string message = ex.ToString();
                error.Text = message;
                Console.WriteLine(message);
            }
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var directories = Directory.EnumerateDirectories("/");
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var directory in directories)
            {
                stringBuilder.AppendLine(directory);
            }
            error.Text = stringBuilder.ToString();
        }
    }
}