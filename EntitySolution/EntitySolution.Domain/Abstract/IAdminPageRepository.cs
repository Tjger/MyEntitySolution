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
       List<Category> LoadAllCategory();
       bool AddNewCategory(Category newCategory);
       bool EditCategory(Category editCategory);
       bool DeleteCategory(int deleteCategoryID);

       List<Item> LoadAllItem();
       bool AddNewItem(Item newItem);
       bool EditItem(Item editItem);
       bool DeleteItem(int deleteItemID);
    }
}
