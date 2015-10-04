using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Concrete
{
    public class EFCategoryRepository : EFBaseRepository, ICategoryRepository
    {
        public List<Category> GetAllCategory()
        {
            List<Category> ret = new List<Category>();
            try
            {
                ret = (from ite in _context.Categories
                       select ite).ToList();
            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }
    }
}
