using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Concrete
{
    public class EFBaseRepository
    {
        public EntityDBContext _context = null;

        public EFBaseRepository()
        {
            _context = new EntityDBContext();
        }

       
    }
}
