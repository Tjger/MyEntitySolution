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
                ErrorHandle.WriteError("GetAllCategory", e.Message);
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
                ErrorHandle.WriteError("GetAllCategory", e.Message);
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
                ErrorHandle.WriteError("GetAllCategory", e.Message);
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
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public List<Item> LoadAllItem(string sItemStatus, string sHotStatus, ref int totalCount,string sCategoryID = "", string sSearchText = "" )
        {
            List<Item> ret = new List<Item>();
            totalCount = 0;
            try
            {
                var _ret = (from ite in _context.Items
                            select ite).ToList();
                if (_ret != null)
                {
                    ret = _ret.Where(e => (sItemStatus == null || sItemStatus == "" || sItemStatus == allValue || e.Active == sItemStatus) 
                        && (sHotStatus == null || sHotStatus == "" || sHotStatus == allValue || e.Hot == sHotStatus)
                         && (sCategoryID == null || sCategoryID == "" || sCategoryID == allValue || e.CategoryID.Value.ToString() == sCategoryID)
                         && (sSearchText == null || sSearchText == "" || sSearchText == allValue || e.ItemName.Contains(sSearchText) || e.ItemName2.Contains(sSearchText) || e.ItemName.Contains(sSearchText.ToUpper()) || e.ItemName2.Contains(sSearchText.ToUpper()))).ToList();

                  
                }
                totalCount = ret.Count;
            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
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
                newItem.ItemCondition = newItem.ItemCondition == null ? "" : newItem.ItemCondition;
                newItem.ItemPrice2 = newItem.ItemPrice2 == null ? "" : newItem.ItemPrice2;
                newItem.ItemCondition2 = newItem.ItemCondition2 == null ? "" : newItem.ItemCondition2;
                newItem.FolderID = newItem.FolderID == null ? "" : newItem.FolderID;
                newItem.Hot = newItem.Hot == null ? "" : newItem.Hot;
                newItem.KeySearch = newItem.KeySearch == null ? "" : newItem.KeySearch;

                newItem.ItemImageURL = newItem.ItemImageURL == null ? "" : newItem.ItemImageURL;
                newItem.ItemImageURL2 = newItem.ItemImageURL2 == null ? "" : newItem.ItemImageURL2;
                newItem.ItemImageURL3 = newItem.ItemImageURL3 == null ? "" : newItem.ItemImageURL3;
                newItem.ItemImageURL4 = newItem.ItemImageURL4 == null ? "" : newItem.ItemImageURL4;
                newItem.Active = newItem.Active == null ? "" : newItem.Active;

                _context.Items.Add(newItem);
                if (_context.SaveChanges() > 0)
                {
                    ret = true;
                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public Item LoadItemByItemID(int sItemID)
        {
            Item ret = new Item();
            try
            {
                ret = (from ite in _context.Items
                       where ite.ItemID == sItemID
                       select ite).FirstOrDefault();

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
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
                    oldItem.ItemCondition = editItem.ItemCondition == null ? "" : editItem.ItemCondition;
                    oldItem.ItemPrice2 = editItem.ItemPrice2 == null ? "" : editItem.ItemPrice2;
                    oldItem.ItemCondition2 = editItem.ItemCondition2 == null ? "" : editItem.ItemCondition2;
                    oldItem.Hot = editItem.Hot == null ? "" : editItem.Hot;
                    oldItem.KeySearch = editItem.KeySearch == null ? "" : editItem.KeySearch;
                    if (editItem.ItemImageURL != null && editItem.ItemImageURL != "")
                    {
                        oldItem.ItemImageURL = editItem.ItemImageURL;
                    }

                    if (editItem.ItemImageURL2 != null && editItem.ItemImageURL2 != "")
                    {
                        oldItem.ItemImageURL2 = editItem.ItemImageURL2;
                    }

                    if (editItem.ItemImageURL3 != null && editItem.ItemImageURL3 != "")
                    {
                        oldItem.ItemImageURL3 = editItem.ItemImageURL3;
                    }

                    if (editItem.ItemImageURL4 != null && editItem.ItemImageURL4 != "")
                    {
                        oldItem.ItemImageURL4 = editItem.ItemImageURL4;
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
                ErrorHandle.WriteError("GetAllCategory", e.Message);
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
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public Item LoadItemAndRelativeOfIt(string sItemID, ref List<Item> listItemRelative)
        {
            Item ret = new Item();

            try
            {
                int sItemIDTem = 1;
                int.TryParse(sItemID, out sItemIDTem);
                var _ret = (from ite in _context.Items
                            where ite.ItemID == sItemIDTem
                            select ite).FirstOrDefault();

                if (_ret != null)
                {
                    ret = _ret;
                    var _retList = (from ite in _context.Items
                                    where ite.CategoryID == _ret.CategoryID && ite.ItemID != _ret.ItemID
                                    select ite).OrderByDescending(p => p.ItemID).ToList();

                    if (_retList != null)
                    {
                        listItemRelative = _retList;
                    }

                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public List<News> LoadAllNews(string sNewsStatus, int numberOfRecord, ref int totalCount)
        {
            List<News> ret = new List<News>();
            try
            {
                var _ret = (from ite in _context.News
                            select ite).OrderByDescending(p => p.CreatedDate).ToList();

                if (_ret != null)
                {
                    totalCount = _ret.Count;
                    if (numberOfRecord == Var.DefaultValueInComboBox)
                    {
                        ret = _ret.Where(e => (sNewsStatus == null || sNewsStatus == "" || sNewsStatus == allValue || e.Active == sNewsStatus)).ToList();

                    }
                    else
                    {
                        ret = _ret.Where(e => (sNewsStatus == null || sNewsStatus == "" || sNewsStatus == allValue || e.Active == sNewsStatus)).Take(numberOfRecord).ToList();

                    }
                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public bool AddNewNews(News newNews)
        {
            bool ret = false;
            try
            {
                newNews.CreatedDate = DateTime.Now;
                newNews.Title = newNews.Title == null ? "" : newNews.Title;

                newNews.Title2 = newNews.Title2 == null ? "" : newNews.Title2;

                newNews.Description = newNews.Description == null ? "" : newNews.Description;
                newNews.Description2 = newNews.Description2 == null ? "" : newNews.Description2;

                newNews.MainContent = newNews.MainContent == null ? "" : newNews.MainContent;

                newNews.MainContent2 = newNews.MainContent2 == null ? "" : newNews.MainContent2;

                newNews.KeySearch = newNews.KeySearch == null ? "" : newNews.KeySearch;
                newNews.ItemImageURL = newNews.ItemImageURL == null ? "" : newNews.ItemImageURL;

                newNews.Active = newNews.Active == null ? "" : newNews.Active;

                _context.News.Add(newNews);
                if (_context.SaveChanges() > 0)
                {
                    ret = true;
                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public News LoadNewsByNewsID(int sNewsID)
        {
            News ret = new News();
            try
            {
                ret = (from ite in _context.News
                       where ite.NewsID == sNewsID
                       select ite).FirstOrDefault();

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public bool EditNews(News editNews)
        {
            bool ret = false;
            try
            {
                News oldItem = (from ite in _context.News
                                where ite.NewsID == editNews.NewsID
                                select ite).FirstOrDefault();

                if (oldItem != null)
                {
                    oldItem.Title = editNews.Title == null ? "" : editNews.Title;

                    oldItem.Title2 = editNews.Title2 == null ? "" : editNews.Title2;


                    oldItem.Title2 = editNews.Title2 == null ? "" : editNews.Title2;

                    oldItem.Description = editNews.Description == null ? "" : editNews.Description;
                    oldItem.Description2 = editNews.Description2 == null ? "" : editNews.Description2;

                    oldItem.MainContent = editNews.MainContent == null ? "" : editNews.MainContent;

                    oldItem.MainContent2 = editNews.MainContent2 == null ? "" : editNews.MainContent2;

                    oldItem.KeySearch = editNews.KeySearch == null ? "" : editNews.KeySearch;


                    oldItem.Active = editNews.Active == null ? "" : editNews.Active;


                    if (editNews.ItemImageURL != null && editNews.ItemImageURL != "")
                    {
                        oldItem.ItemImageURL = editNews.ItemImageURL;
                    }

                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }

                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public bool DeleteNews(int deleteNewsID)
        {
            bool ret = false;
            try
            {
                News oldItem = (from ite in _context.News
                                where ite.NewsID == deleteNewsID
                                select ite).FirstOrDefault();

                if (oldItem != null)
                {
                    _context.News.Remove(oldItem);
                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }
                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public News LoadNewsAndRelativeOfIt(string sNewsID, int numberOfRecordRelative, ref List<News> listNewsAndRelative)
        {
            News ret = new News();

            try
            {
                int sNewsIDTem = 1;
                int.TryParse(sNewsID, out sNewsIDTem);
                var _ret = (from ite in _context.News
                            where ite.NewsID == sNewsIDTem
                            select ite).FirstOrDefault();

                if (_ret != null)
                {
                    ret = _ret;
                    var _retList = (from ite in _context.News
                                    where ite.CreatedDate <= _ret.CreatedDate && ite.NewsID != _ret.NewsID
                                    select ite).Take(numberOfRecordRelative).OrderByDescending(p => p.CreatedDate).ToList();

                    if (_retList != null)
                    {
                        listNewsAndRelative = _retList;
                    }
                     
                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }

        public SysPara GetSysPara(string fieldSysPara)
        {
            SysPara ret = new SysPara();
            ret.Field = fieldSysPara;
            ret.Value = "";
            ret.DefaultValue = "";
            try
            {
                var _ret = (from ite in _context.SysParas
                            where ite.Field == fieldSysPara
                            select ite).FirstOrDefault();
                if (_ret != null)
                {
                    ret = _ret;
                }
            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public bool EditSysPara(SysPara editSysPara)
        {
            bool ret = false;
            try
            {
                SysPara oldItem = (from ite in _context.SysParas
                                   where ite.Field == editSysPara.Field
                                   select ite).FirstOrDefault();

                if (oldItem != null)
                {
                    oldItem.Value = editSysPara.Value == null ? "" : editSysPara.Value;

                    oldItem.DefaultValue = editSysPara.DefaultValue == null ? "" : editSysPara.DefaultValue;

                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }

                }
                else
                {
                    _context.SysParas.Add(editSysPara);
                    if (_context.SaveChanges() > 0)
                    {
                        ret = true;
                    }
                }

            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }
        public bool SetSysPara()
        {
            bool ret = true;
            try
            {
                Var.SMTPHost = InitSysPara(Var.SMTPHostField, "smtp.gmail.com");
                Var.SMTPEmailAddress = InitSysPara(Var.SMTPEmailAddressField, "sales@tanphongcontainer.vn");
                Var.SMTPEmailPassword = InitSysPara(Var.SMTPEmailPasswordField, "smtp.gmail.com");
                Var.SMTPEmailManager = InitSysPara(Var.SMTPEmailManagerField, "sales@tanphongcontainer.vn");
            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }


        public string InitSysPara(string fieldSysPara, string defaultValueSysPara)
        {
            string ret = "";
            try
            {
                var _ret = (from ite in _context.SysParas
                            where ite.Field == fieldSysPara
                            select ite).FirstOrDefault();
                if (_ret != null)
                {
                    ret = _ret.Value;
                }
                else
                {
                    SysPara sys = new SysPara();
                    sys.Field = fieldSysPara;
                    sys.Value = defaultValueSysPara;
                    sys.DefaultValue = defaultValueSysPara;

                    _context.SysParas.Add(sys);
                    if (_context.SaveChanges() > 0)
                    {
                        ret = defaultValueSysPara;
                    }
                }
            }
            catch (Exception e)
            {
                ErrorHandle.WriteError("GetAllCategory", e.Message);
            }
            return ret;
        }

    }
}
