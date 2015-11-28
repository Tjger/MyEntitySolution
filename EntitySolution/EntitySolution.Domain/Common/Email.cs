using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
 

namespace EntitySolution.Domain.Common
{
    public class Email
    {
        private string RwMailServer = "smtp.gmail.com";
        private string RwMailAccount = "";
        private string RwMailPassword = "";
        private string RwMailSender = "";
        private string RwMailEndcoding = "UTF-8";
        private bool RwMailEnableSSL = false;
        private int RwMailPort = -1;
        private object lockObj = new object();

        public Email()
        {

            RwMailServer = Var.SMTPHost;
            RwMailAccount = Var.SMTPEmailAddress;
            RwMailPassword = Var.SMTPEmailPassword;
            RwMailSender = Var.SMTPEmailAddress;
            RwMailPort = Var.SMTPPort;
            RwMailEnableSSL = true;

        }

        public bool SendMail(MailMessage mailMessage)
        {
            bool bFlag = false;
            try
            {
                lock (lockObj)
                {
                    if (RwMailServer == "" || RwMailAccount == "" || RwMailPassword == "")
                    {
                        return false;
                    }

                    System.Net.Mail.SmtpClient smptclient = new System.Net.Mail.SmtpClient(RwMailServer);
                    smptclient.UseDefaultCredentials = false;
                    smptclient.EnableSsl = RwMailEnableSSL;
                    if (RwMailPort != -1)
                    {
                        smptclient.Port = RwMailPort;
                    }

                    smptclient.Credentials = new System.Net.NetworkCredential(RwMailAccount, RwMailPassword);
                    smptclient.SendMailAsync(mailMessage);
                    //smptclient.Send(mailMessage);
                    bFlag = true;

                }
            }
            catch (Exception exc)
            {
                ErrorHandle.WriteError(exc.ToString());
            }
            return bFlag;
        }

        public MailMessage BuildMailMessage(string sToAddress, string sSubject, string sBody, Boolean IsBodyHtml)
        {
            System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(RwMailSender);
            System.Net.Mail.MailAddress toAddress;
            string mail = string.Empty;
            //if (Var.DevEnvironment)
            //{

            //    toAddress = new System.Net.Mail.MailAddress(Var.DevEmail);
            //}
            //else
            //{
            //    toAddress = new System.Net.Mail.MailAddress(sToAddress);
            //}

            toAddress = new System.Net.Mail.MailAddress(sToAddress);
            mailMessage.From = fromAddress;
            mailMessage.To.Add(toAddress);
            mailMessage.Subject = sSubject;
            mailMessage.IsBodyHtml = IsBodyHtml;
            mailMessage.Body = sBody;
            mailMessage.BodyEncoding = Encoding.GetEncoding(RwMailEndcoding);
            return mailMessage;
        }



    }
}
