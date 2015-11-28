using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Common
{
    public class Core
    {
        private static string EncryptionKey = "tungtien2161986";

        public static string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {0x49,0x76,0x61,0x6e,
			0x20,0x4d,0x65,0x64,0x76,0x65,0x64,0x65,0x76
                });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        public static string Decrypt(string cipherText)
        {          
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] {
			0x49,
			0x76,
			0x61,
			0x6e,
			0x20,
			0x4d,
			0x65,
			0x64,
			0x76,
			0x65,
			0x64,
			0x65,
			0x76
		});
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        public static void SendEmail(string EmailAddress,string Subject,string Body)
        {
            try
            {
                Thread oThread1 = new Thread(new ThreadStart(() => ProcessSendEmail(EmailAddress, Subject, Body)));
                oThread1.Start();
                //ProcessSendEmail(emailInfo, emailType, empID);
            }
            catch (Exception ex)
            {
                ErrorHandle.WriteError(ex.Message);
            }
        }

        private static void ProcessSendEmail(string EmailAddress, string Subject, string Body)
        {
            bool bFlag = false;
             
            try
            {
                if (EmailAddress != null && EmailAddress != "" && Subject != null && Subject != "" && Body != null && Body != "")
                {

                    Email em = new Email();
                    System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
                    mailMessage = em.BuildMailMessage(EmailAddress, Subject, Body, true);
                    bFlag = em.SendMail(mailMessage);
                }
            }
            catch (Exception ex)
            {
                ErrorHandle.WriteError(ex.Message);
            }
            
        }
    }
}
