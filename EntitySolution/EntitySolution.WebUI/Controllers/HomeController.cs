using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntitySolution.Domain.Common;
using EntitySolution.Domain.Common.Paging;

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

        public ActionResult ProductDetails()
        {
            var id = RouteData.Values["id"];
            ViewBag.ProductID = "";
            if (id != null)
            {
                ViewBag.ProductID = id;
            }
            return View();
        }


        public JsonResult LoadAllDataForHomePage()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                int totalCount = 0;
                var CategoryList = adminPageProvider.LoadAllCategory(allValue);
                var ItemList = adminPageProvider.LoadAllItem(activeValue, allValue, ref totalCount);
                var NewsList = adminPageProvider.LoadAllNews(activeValue, 3, ref totalCount);

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
                int  totalCount = 0;
                jResult = Json(new { success = true, returnList = adminPageProvider.LoadAllItem(sItemStatus, sHotStatus, ref totalCount) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

     
        public JsonResult LoadAllNews(string sNewsStatus, int numberOfRecord, int sPageIndex)
        {
            JsonResult jResult = new JsonResult();
            try
            {
               int totalCount = 0;
                IList<News> lstItem = adminPageProvider.LoadAllNews(sNewsStatus, Var.DefaultValueInComboBox, ref totalCount);
                IPagedList<News> lstReturn = lstItem.ToPagedList(sPageIndex, Var.PageSize, totalCount);

                jResult = Json(new { success = true, returnList = lstReturn, PageCount = lstReturn.PageCount, HasPreviousPage = lstReturn.HasPreviousPage, HasNextPage = lstReturn.HasNextPage }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }
    }
}