using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntitySolution.Domain.Concrete
{
    public class EFAdminPageRepository : EFBaseRepository, IAdminPageRepository
    {
        public List<Category> LoadAllCategory()
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

        public bool AddNewCategory(Category newCategory)
        {
            bool ret = false;
            try
            {
                _context.Categories.Add(newCategory);
                if (_context.SaveChanges() > 0)
                {
                    ret = true;
                }

            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public bool EditCategory(Category editCategory)
        {
            bool ret = false;
            try
            {
                Category oldCategory = (from ite in _context.Categories
                                        where ite.CategoryID == editCategory.CategoryID
                                        select ite).FirstOrDefault();

                if (oldCategory != null)
                {
                    oldCategory.CategoryName = editCategory.CategoryName;
                    oldCategory.CategoryName2 = editCategory.CategoryName2;
                    oldCategory.Active = editCategory.Active;

                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }

                }

            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public bool DeleteCategory(int deleteCategoryID)
        {
            bool ret = false;
            try
            {
                Category oldCategory = (from ite in _context.Categories
                                        where ite.CategoryID == deleteCategoryID
                                        select ite).FirstOrDefault();

                if (oldCategory != null)
                {
                    Item lstItem = (from ite in _context.Items
                                    where ite.CategoryID == deleteCategoryID
                                    select ite).FirstOrDefault();
                    if (lstItem == null)
                    {
                        _context.Categories.Remove(oldCategory);
                        if (_context.SaveChanges() > 0)
                        {
                            ret = true;
                        }

                    }

                }

            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public List<Item> LoadAllItem()
        {
            List<Item> ret = new List<Item>();
            try
            {
                ret = (from ite in _context.Items
                       select ite).ToList();
            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public bool AddNewItem(Item newItem)
        {
            bool ret = false;
            try
            {
                _context.Items.Add(newItem);
                if (_context.SaveChanges() > 0)
                {
                    ret = true;
                }

            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public bool EditItem(Item editItem)
        {
            bool ret = false;
            try
            {
                Item oldItem = (from ite in _context.Items
                                        where ite.ItemID == editItem.ItemID
                                        select ite).FirstOrDefault();

                if (oldItem != null)
                {
                    oldItem.ItemName = editItem.ItemName;
                    oldItem.CategoryID = editItem.CategoryID;
                    oldItem.Description = editItem.Description;
                    oldItem.ItemPrice = editItem.ItemPrice;
                    oldItem.Hot = editItem.Hot;
                    oldItem.KeySearch = editItem.KeySearch;
                    oldItem.ItemImageURL = editItem.ItemImageURL;
                    oldItem.ItemName2 = editItem.ItemName2;
                    oldItem.Active = editItem.Active;

                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }

                }

            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public bool DeleteItem(int deleteItemID)
        {
            bool ret = false;
            try
            {
                Item oldItem = (from ite in _context.Items
                                where ite.ItemID == deleteItemID
                                select ite).FirstOrDefault();

                if (oldItem != null)
                {
                    _context.Items.Remove(oldItem);
                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }
                }

            }
            catch (Exception e)
            {
                Logging.LogError("GetAllCategory", e.Message);
            }
            return ret;
        }
    }
}
