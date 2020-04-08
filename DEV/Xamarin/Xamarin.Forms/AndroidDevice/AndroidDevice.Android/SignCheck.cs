using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Security;
using Java.Security.Cert;
using Signature = Android.Content.PM.Signature;

namespace AndroidDevice.Droid
{
    /// <summary>
    /// 验证签名
    /// </summary>
    public class SignCheck
    {
        private Context context;
        /// <summary>
        /// 应用的签名
        /// </summary>
        public String cer = null;
        /// <summary>
        /// 真实的证书
        /// </summary>
        private String realCer = null;
        private static String TAG = "SignCheck";

        public SignCheck(Context context)
        {
            this.context = context;
            this.cer = GetCertificateSHA1Fingerprint();
        }

        public SignCheck(Context context, String realCer)
        {
            this.context = context;
            this.realCer = realCer;
            this.cer = GetCertificateSHA1Fingerprint();
        }

        public String getRealCer()
        {
            return realCer;
        }

        /// <summary>
        /// 获取应用的签名
        /// </summary>
        /// <returns></returns>
        public String GetCertificateSHA1Fingerprint()
        {
            //获取包管理器
            PackageManager pm = context.PackageManager;

            //获取当前要获取 SHA1 值的包名，也可以用其他的包名，但需要注意，
            //在用其他包名的前提是，此方法传递的参数 Context 应该是对应包的上下文。
            String packageName = context.PackageName;

            //返回包括在包中的签名信息。。还可以获取签名信息等 PackageInfoFlags.SigningCertificates
            var packageInfo = pm.GetPackageInfo(context.PackageName, PackageInfoFlags.Signatures);

            //签名信息
            List<Signature> signatures = packageInfo.Signatures?.ToList();
            if (signatures == null || signatures.Count == 0)
            {
                return "";
            }
            byte[] cert = signatures[0].ToByteArray();

            //将签名转换为字节数组流
            //InputStream input = new ByteArrayInputStream(cert);
            System.IO.Stream input = new MemoryStream(cert);

            //证书工厂类，这个类实现了出厂合格证算法的功能
            CertificateFactory cf = null;

            try
            {
                cf = CertificateFactory.GetInstance("X509");
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(TAG + e.StackTrace);
            }

            //X509 证书，X.509 是一种非常通用的证书格式
            X509Certificate c = null;

            try
            {
                c = (X509Certificate)cf.GenerateCertificate(input);
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(TAG + e.StackTrace);
            }

            String hexString = null;

            try
            {
                //加密算法的类，这里的参数可以使 MD4,MD5 等加密算法
                MessageDigest md = MessageDigest.GetInstance("SHA1");

                //获得公钥
                byte[] publicKey = md.Digest(c.GetEncoded());

                //字节到十六进制的格式转换
                hexString = byte2HexFormatted(publicKey);

            }
            catch (NoSuchAlgorithmException e)
            {
                System.Diagnostics.Debug.WriteLine(TAG + e.StackTrace);
            }
            catch (CertificateEncodingException e)
            {
                System.Diagnostics.Debug.WriteLine(TAG + e.StackTrace);
            }
            return hexString;
        }

        //这里是将获取到得编码进行16 进制转换
        private String byte2HexFormatted(byte[] arr)
        {

            StringBuilder str = new StringBuilder(arr.Length * 2);

            for (int i = 0; i < arr.Length; i++)
            {
                String h = arr[i].ToString("X2");
                int l = h.Length;
                if (l == 1)
                    h = "0" + h;
                if (l > 2)
                    h = h.Substring(l - 2, l);
                str.Append(h.ToUpper());
                if (i < (arr.Length - 1))
                    str.Append(':');
            }
            return str.ToString();
        }


        /// <summary>
        /// 检测签名是否正确
        /// </summary>
        /// <returns></returns>
        public bool check()
        {
            if (this.realCer != null)
            {
                cer = cer.Trim();
                realCer = realCer.Trim();
                if (this.cer.Equals(this.realCer))
                {
                    return true;
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(TAG + "未给定真实的签名 SHA-1 值");
            }
            return false;
        }
    }
}