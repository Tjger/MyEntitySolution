using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Abstract
{
   public interface IAdminPageRepository
    {
       List<Category> GetAllCategory();
    }
}
