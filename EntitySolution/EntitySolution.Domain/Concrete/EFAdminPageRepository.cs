using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntitySolution.Domain.Common;

namespace EntitySolution.Domain.Concrete
{
    public class EFAdminPageRepository : EFBaseRepository, IAdminPageRepository
    {
        private string allValue = Var.DefaultValueInComboBox.ToString();

        public List<Category> LoadAllCategory(string sCategoryStatus)
        {
            List<Category> ret = new List<Category>();
            try
            {
                ret = (from ite in _context.Categories
                       where (sCategoryStatus == null || sCategoryStatus == "" || sCategoryStatus == allValue || ite.Active == sCategoryStatus)
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

        public List<Item> LoadAllItem(string sItemStatus)
        {
            List<Item> ret = new List<Item>();
            try
            {
                ret = (from ite in _context.Items
                       where (sItemStatus == null || sItemStatus == "" || sItemStatus == allValue || ite.Active == sItemStatus)
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
                newItem.CreatedDate = DateTime.Now;
                newItem.ItemName = newItem.ItemName == null ? "" : newItem.ItemName;
                newItem.ItemName2 = newItem.ItemName2 == null ? "" : newItem.ItemName2;

                newItem.CategoryName = newItem.CategoryName == null ? "" : newItem.CategoryName;
                newItem.Description = newItem.Description == null ? "" : newItem.Description;
                newItem.Description2 = newItem.Description2 == null ? "" : newItem.Description2;
                newItem.ItemPrice = newItem.ItemPrice == null ? "" : newItem.ItemPrice;
                newItem.Hot = newItem.Hot == null ? "" : newItem.Hot;
                newItem.KeySearch = newItem.KeySearch == null ? "" : newItem.KeySearch;
                newItem.ItemImageURL = newItem.ItemImageURL;

                newItem.Active = newItem.Active == null ? "" : newItem.Active;

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

        public Item LoadItemByItemID(int sItemID)
        {
            Item ret = null;
            try
            {
                ret = (from ite in _context.Items
                       where ite.ItemID == sItemID
                       select ite).FirstOrDefault();

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
                    oldItem.ItemName = editItem.ItemName == null ? "" : editItem.ItemName;
                    oldItem.CategoryID = editItem.CategoryID;
                    oldItem.CategoryName = editItem.CategoryName == null ? "" : editItem.CategoryName;
                    oldItem.Description = editItem.Description == null ? "" : editItem.Description;
                    oldItem.Description2 = editItem.Description2 == null ? "" : editItem.Description2;
                    oldItem.ItemPrice = editItem.ItemPrice == null ? "" : editItem.ItemPrice;
                    oldItem.Hot = editItem.Hot == null ? "" : editItem.Hot;
                    oldItem.KeySearch = editItem.KeySearch == null ? "" : editItem.KeySearch;
                    if (editItem.ItemImageURL != null && editItem.ItemImageURL != "")
                    {
                        oldItem.ItemImageURL = editItem.ItemImageURL;
                    }

                    oldItem.ItemName2 = editItem.ItemName2 == null ? "" : editItem.ItemName2;
                    oldItem.Active = editItem.Active == null ? "" : editItem.Active;

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
