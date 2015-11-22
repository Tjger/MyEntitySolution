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
        public enum SystemStatus
        {
            Inactive = 0,
            Active = 1,
         
        }
    }
}
