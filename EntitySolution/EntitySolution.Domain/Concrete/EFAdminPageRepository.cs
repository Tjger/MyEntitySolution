﻿using EntitySolution.Domain.Abstract;
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

        public bool SaveCategory(Category newCategory)
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
    }
}
