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
        public bool Authenticate(string Username, string Password)
        {
            bool ret = false;
            try
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
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return ret;
        }
    }
}
