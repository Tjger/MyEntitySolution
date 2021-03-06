﻿using EntitySolution.Domain.Abstract;
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
            if (TempData["CategoryIndex"] != null)
            {

                ViewBag.DefaultCategory = TempData["CategoryIndex"];

            }
            else
            {
                ViewBag.DefaultCategory = "0";
            }
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
            var id = RouteData.Values["id"];

            ViewBag.ListNewsAndRelative = new List<News>();
            List<News> ListNewsAndRelative = new List<News>();
            News NewsDetail = new News();
            if (id != null)
            {

                NewsDetail = adminPageProvider.LoadNewsAndRelativeOfIt(id.ToString(), 5, ref ListNewsAndRelative);
                ViewBag.ListNewsAndRelative = ListNewsAndRelative;

            }
            return View(NewsDetail);
        }

        public ActionResult Product()
        {

            return View();
        }

        public ActionResult ProductDetails()
        {
            var id = RouteData.Values["id"];
            ViewBag.ListProductAndRelative = new List<Item>();
            List<Item> ListProductAndRelative = new List<Item>();
            Item ItemDetail = new Item();
            if (id != null)
            {
                ItemDetail = adminPageProvider.LoadItemAndRelativeOfIt(id.ToString(), ref ListProductAndRelative);
                ViewBag.ListProductAndRelative = ListProductAndRelative;
            }
            return View(ItemDetail);
        }

        public ActionResult OrganizationalStructures()
        {
            return View();
        }

        public ActionResult OrientedDevelopment()
        {
            return View();
        }

        public ActionResult TypicalProjects()
        {
            return View();
        }

        public ActionResult Services()
        {
            return View();
        }

        public ActionResult FeaturedProducts()
        {
            return View();
        }


        public ActionResult Search(string search, string category)
        {



            if (search != null && search.Trim() != "")
            {
                ViewBag.SearchText = search;
                ViewBag.CategoryID = category;
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        public JsonResult LoadAllDataForHomePage()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                int totalCount = 0;

                var ItemListHot = adminPageProvider.LoadAllItem(allValue, activeValue, ref totalCount);
                var NewsList = adminPageProvider.LoadAllNews(activeValue, 3, ref totalCount);

                jResult = Json(new { success = true, NewsList = NewsList, ItemListHot = ItemListHot }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadAllDataForIndexPage()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                int totalCount = 0;

                var ItemListFeatured = adminPageProvider.LoadAllItem(activeValue, allValue, ref totalCount);

                
                jResult = Json(new { success = true, ItemListFeatured = ItemListFeatured }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }


        public JsonResult LoadAllDataProducts()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                int totalCount = 0;
                var CategoryList = adminPageProvider.LoadAllCategory(allValue);
                var ItemList = adminPageProvider.LoadAllItem(allValue, allValue, ref totalCount);


                var ItemInCategory = new List<Item>[CategoryList.Capacity];

                for (int i = 0; i < CategoryList.Capacity; i++)
                {
                    var ItemInCat = ItemList.Where(e => e.CategoryID == CategoryList[i].CategoryID).ToList();
                    ItemInCategory[i] = ItemInCat;
                }

                jResult = Json(new { success = true, ItemInCategory = ItemInCategory }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadAllFeaturedProducts()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                int totalCount = 0;
                var CategoryList = adminPageProvider.LoadAllCategory(allValue);
                var ItemList = adminPageProvider.LoadAllItem(activeValue, allValue, ref totalCount);


                var ItemInCategory = new List<Item>[CategoryList.Capacity];

                for (int i = 0; i < CategoryList.Capacity; i++)
                {
                    var ItemInCat = ItemList.Where(e => e.CategoryID == CategoryList[i].CategoryID).ToList();
                    ItemInCategory[i] = ItemInCat;
                }

                jResult = Json(new { success = true, ItemInCategory = ItemInCategory }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }


        public JsonResult SetDefaultCategory(string CategoryIndex)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                TempData["CategoryIndex"] = CategoryIndex;
                jResult = Json(new { success = true }, JsonRequestBehavior.AllowGet);

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

        public JsonResult LoadAllItem(int sPageIndex, string sItemStatus, string sHotStatus, string sSearchText, string sCategoryID)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                int totalCount = 0;
                IList<Item> lstItem = adminPageProvider.LoadAllItem(sItemStatus, sHotStatus, ref totalCount, sCategoryID, sSearchText);
                IPagedList<Item> lstReturn = lstItem.ToPagedList(sPageIndex, Var.PageSize, totalCount);

                jResult = Json(new { success = true, returnList = lstReturn, PageCount = lstReturn.PageCount, HasPreviousPage = lstReturn.HasPreviousPage, HasNextPage = lstReturn.HasNextPage }, JsonRequestBehavior.AllowGet);

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


        public JsonResult SendEmail(string EmailAddress, string Subject, string Body)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = Core.ProcessSendEmail(Var.SMTPEmailManager, Subject, Body) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }
    }
}