using EntitySolution.Domain.Abstract;
using EntitySolution.Domain.Common;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Concrete
{
    public class EFAuthenticateRepository : EFBaseRepository, IAuthenticateRepository
    {
        public bool Authenticate(string Username, string Password, ref bool isSuperAdmin, ref string sUserID, ref string sEmail)
        {
            bool ret = false;
            try
            {
                if (Username.ToLower() == Var.SuperAdminLoginID.ToLower() && Password.ToLower() == Var.SuperAdminPassword)
                {
                    ret = true;
                    isSuperAdmin = true;
                    sUserID = "1986";
                }
                else
                {
                    Emp e = (from it in _context.Emps
                             where it.LoginID.ToLower() == Username.ToLower()
                             select it).FirstOrDefault();
                    if (e != null)
                    {
                        string PasswordEncrypt = Core.Encrypt(Password);
                        if (e.Pass == PasswordEncrypt)
                        {
                            ret = true;
                            isSuperAdmin = false;
                            sUserID = e.EmpID.ToString();
                            sEmail = e.Email;
                        }
                    }
                }
              
            }
            catch (Exception)
            {

               
            }
            return ret;
        }
    }
}
