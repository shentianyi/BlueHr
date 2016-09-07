using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BlueHrLib.Helper
{
    public class EmailHelper
    {
        /// <summary>
        /// 发送邮件
        /// </summary>
        /// <param name="host"></param>
        /// <param name="user"></param>
        /// <param name="address"></param>
        /// <param name="pwd"></param>
        /// <param name="toEmails">收件人地址，多个以逗号分隔</param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="isHtml"></param>
        public static void SendEmail(string host, string user, string address, string pwd, string toEmails, string subject, string body,bool isHtml=false)
        {
            if (string.IsNullOrEmpty(host) || string.IsNullOrEmpty(toEmails))
            {
                return;
            }
            using (SmtpClient server = new SmtpClient(host))
            {
                server.Credentials = new NetworkCredential(user, pwd);
                using (MailMessage mail = new MailMessage())
                {
                    mail.From = new MailAddress(address);
                    mail.To.Add(toEmails);
                    mail.Subject = subject;
                    mail.Body = body;
                    //内容编码、格式
                    mail.BodyEncoding = System.Text.Encoding.UTF8;
                    mail.IsBodyHtml = isHtml;

                    server.Send(mail);
                }
            }
        }


        /// <summary>
        /// 根据template生产邮件内容
        /// </summary>
        /// <param name="templateName"></param>
        /// <param name="values"></param>
        /// <param name="prefix"></param>
        /// <param name="postfix"></param>
        /// <returns></returns>
        public static string Build(string templateName, Dictionary<string,string> values=null, string prefix = "$", string postfix = "$")
        {
            string template = string.Empty;
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FileHelper.EMAIL_TEMPLATE_PATH, templateName);
            using (StreamReader reader = new StreamReader(path))
            {
                template = reader.ReadToEnd();


                if (values != null)
                {
                    foreach (var entry in values)
                    {
                        template = template.Replace(string.Format("{0}{1}{2}", prefix, entry.Key, postfix), entry.Value.ToString());
                    }
                }
                return template;

            }
        }
    

    }
}
