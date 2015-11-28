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
       List<Category> LoadAllCategory(string sCategoryStatus);
       bool AddNewCategory(Category newCategory);
       bool EditCategory(Category editCategory);
       bool DeleteCategory(int deleteCategoryID);

       List<Item> LoadAllItem(string sItemStatus, string sHotStatus, ref int totalCount);
       Item LoadItemByItemID(int sItemID);
       bool AddNewItem(Item newItem);
       bool EditItem(Item editItem);
       bool DeleteItem(int deleteItemID);
       Item LoadItemAndRelativeOfIt(string sItemID, ref List<Item> listItemRelative);

       List<News> LoadAllNews(string sNewsStatus, int numberOfRecord, ref int totalCount);
       News LoadNewsByNewsID(int sNewsID);
       bool AddNewNews(News newNews);
       bool EditNews(News editNews);
       bool DeleteNews(int deleteNewsID);
       News LoadNewsAndRelativeOfIt(string sNewsID, int numberOfRecordRelative, ref List<News> listNewsAndRelative);

       SysPara GetSysPara(string fieldSysPara);
       bool EditSysPara(SysPara editSysPara);
    }
}
