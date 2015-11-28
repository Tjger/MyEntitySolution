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
        public static int PageSize =4;

        public static string SMTPHost = "";
        public static string SMTPEmailAddress = "";
        public static string SMTPEmailPassword = "";
        public static string SMTPEmailSenderName = "";
        public static string SMTPEmailManager = "";
        public static bool SMTPEnableSSL = true;
        public static int SMTPPort = -1;

        public enum SystemStatus
        {
            Inactive = 0,
            Active = 1,
         
        }
    }
}
