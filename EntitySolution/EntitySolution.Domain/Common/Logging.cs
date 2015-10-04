using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace EntitySolution.Domain
{
    public class Logging 
    {
        public static void LogError(string sClassName, string sMsgErr)
        {
            try
            {
                // You could use any logging approach here
                var _file_name = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                var _file_path = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\" + "Log\\" + _file_name;
                if (!File.Exists(_file_path))
                {
                    File.Create(_file_path);
                }

                //StreamReader _read;
                //_read = new StreamReader(_file_path);
                //if (_read.ReadToEnd().Trim().Length < 1)
                //{
                //    _read.Close();
                //    string _file_log = string.Empty;
                //    _file_log = "Error Details/ Output   ---   " + Server.GetLastError().Message.ToString() + ";    Execution Time:   ---    " + System.DateTime.Now;
                //    StreamWriter _wrt;
                //    _wrt = File.CreateText(_file_path.ToString());
                //    _wrt.WriteLine(_file_log.ToString());
                //    _wrt.Close();
                //}

                var sw = new System.IO.StreamWriter(_file_path, true);
                sw.WriteLine(DateTime.Now.ToString() + ": " + sClassName + "[ " + sMsgErr + "]");
                sw.Close();
            }
            catch (Exception e)
            {
                
                throw;
            }
            

        }
    }
}
