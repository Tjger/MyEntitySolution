using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitySolution.Domain.Common;

namespace EntitySolution.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private string allValue = Var.DefaultValueInComboBox.ToString();
        private string activeValue = ((int)Var.SystemStatus.Active).ToString();

        private IAdminPageRepository categoryProvider;
        private IAdminPageRepository adminPageProvider;
        public HomeController(IAdminPageRepository categoryRepository, IAdminPageRepository adminPageRepository)
        {
            categoryProvider = categoryRepository;
            adminPageProvider = adminPageRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
             
            return View();
        }

        public ActionResult Contact()
        {
             
            return View();
        }

        public ActionResult News()
        {
            
            return View();
        }

        public ActionResult NewsDetail()
        {

            return View();
        }

        public JsonResult LoadAllDataForHomePage()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                var CategoryList = adminPageProvider.LoadAllCategory(allValue);
                var ItemList = adminPageProvider.LoadAllItem(activeValue, allValue);
                var NewsList = adminPageProvider.LoadAllNews(activeValue, 3);

                var ItemInCategory = new List<Item>[CategoryList.Capacity];

                for (int i = 0; i < CategoryList.Capacity; i++)
                {
                     var ItemInCat = ItemList.Where(e => e.CategoryID == CategoryList[i].CategoryID).ToList();
                     ItemInCategory[i] = ItemInCat;
                }
                
                var ItemListHot = ItemList.Where(e => e.Hot == activeValue).ToList();
                jResult = Json(new { success = true, CategoryList = CategoryList, NewsList = NewsList, ItemListHot = ItemListHot, ItemInCategory = ItemInCategory }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadAllCategory(string sCategoryStatus)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.LoadAllCategory(sCategoryStatus) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadAllItem(string sItemStatus, string sHotStatus)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.LoadAllItem(sItemStatus, sHotStatus) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadAllNews(string sNewsStatus, int numberOfRecord)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.LoadAllNews(sNewsStatus, numberOfRecord) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }
    }
}