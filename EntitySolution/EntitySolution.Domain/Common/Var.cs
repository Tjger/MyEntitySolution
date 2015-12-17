using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Common
{
    public class Var
    {
        public static string SuperAdminLoginID = "tientung";
        public static string SuperAdminPassword = "tungtien";
        public static string FormatDate = "dd/MM/yyyy";
        public static string UrlUploadItemImage = "~/Content/Upload/images";
        public static string UrlUploadCompanyNewsImage = "~/Content/Upload/images/TinCongTy";
        public static int DefaultValueInComboBox = -1;

        public static string ConfigAbout = "ConfigAbout";
        public static string ConfigContacts = "ConfigContacts";
        public static string CultureLanguage = "vi-VN";
        public static int PageSize =8;

      
        public static string SMTPHost = "smtp.gmail.com";
        public static string SMTPEmailAddress = "nstung@paradigmsft.com";
        public static string SMTPEmailPassword = "nst2161986";
        public static string SMTPEmailSenderName = "Tj";
        public static string SMTPEmailManager = "sales@tanphongcontainer.vn";
        public static bool SMTPEnableSSL = true;
        public static int SMTPPort = 587;
         
        public enum SystemStatus
        {
            Inactive = 0,
            Active = 1,
         
        }
    }
}
