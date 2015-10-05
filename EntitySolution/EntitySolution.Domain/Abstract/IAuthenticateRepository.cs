using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Abstract
{
    public interface IAuthenticateRepository
    {
        bool Authenticate(string Username, string Password);
    }
}
