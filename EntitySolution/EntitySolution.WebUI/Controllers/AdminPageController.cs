using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using EntitySolution.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using EntitySolution.Domain.Common;
using EntitySolution.Domain.Common.Paging;
using System.IO;
namespace EntitySolution.WebUI.Controllers
{
    public class AdminPageController : Controller
    {
        int totalCount = 0;
        private string activeStatus = ((int)Var.SystemStatus.Active).ToString();
        private string allStatus = ((int)Var.DefaultValueInComboBox).ToString();
        private IAdminPageRepository adminPageProvider;
        private IAuthenticateRepository authenticateProvider;
        public AdminPageController(IAuthenticateRepository authenticateRepository, IAdminPageRepository adminPageRepository)
        {
            authenticateProvider = authenticateRepository;
            adminPageProvider = adminPageRepository;
        }

        [AllowAnonymous]
        public ActionResult Login()
        {
            LoginObjectEntity model = new LoginObjectEntity();
            return View(model);

        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginObjectEntity model)
        {
            if (ModelState.IsValid)
            {

                if (model != null)
                {
                    if (model.Password != "" && model.UserName != "")
                    {
                        bool isSuperAdmin = false;
                        string sUserID = "";
                        if (authenticateProvider.Authenticate(model.UserName, model.Password, ref isSuperAdmin, ref sUserID))
                        {
                            Session["EmpID"] = sUserID;
                            if (isSuperAdmin)
                            {
                                Session["isSuperAdmin"] = "1";
                            }
                            else
                            {
                                Session["isSuperAdmin"] = "0";
                            }
                            return RedirectToAction("Category");
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }


        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }

        public ActionResult Receipt()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Category()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Item()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult News()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult Config()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult UserInformation()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult ConfigAbout()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        public ActionResult ConfigContacts()
        {
            if (Session["EmpID"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
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

        public JsonResult AddNewCategory(Category categoryInfor)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = adminPageProvider.AddNewCategory(categoryInfor), returnList = adminPageProvider.LoadAllCategory(allStatus) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult EditCategory(Category categoryInfor)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = adminPageProvider.EditCategory(categoryInfor), returnList = adminPageProvider.LoadAllCategory(allStatus) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult DeleteCategory(int deleteCategoryID)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = adminPageProvider.DeleteCategory(deleteCategoryID), returnList = adminPageProvider.LoadAllCategory(allStatus) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }


        public JsonResult LoadAllItem(string sItemStatus, int sPageIndex)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                IList<Item> lstItem = adminPageProvider.LoadAllItem(sItemStatus, Var.DefaultValueInComboBox.ToString(), ref totalCount);
                IPagedList<Item> lstReturn = lstItem.ToPagedList(sPageIndex, Var.PageSize, totalCount);

                jResult = Json(new { success = true, returnList = lstReturn, PageCount = lstReturn.PageCount, HasPreviousPage = lstReturn.HasPreviousPage, HasNextPage = lstReturn.HasNextPage }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadItemByItemID(int sItemID)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.LoadItemByItemID(sItemID) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult AddNewItem(Item itemInfor)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                bool isUploaded = false;
                string message = "File upload failed";
                if (TempData["HttpPostedFileBase"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase"];
                    string pathForSaving = Server.MapPath(Var.UrlUploadItemImage);
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));
                            isUploaded = true;
                            message = "File uploaded successfully!";
                            itemInfor.ItemImageURL = "/" + file.FileName;
                            TempData["HttpPostedFileBase"] = null;
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                }

                bool ret = adminPageProvider.AddNewItem(itemInfor);
                if (ret)
                {
                    return LoadAllItem(allStatus, 0);
                }
                else
                {
                    jResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult EditItem(Item itemInfor, int sPageIndex)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                bool isUploaded = false;
                string message = "File upload failed";
                if (TempData["HttpPostedFileBase"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase"];
                    string pathForSaving = Server.MapPath(Var.UrlUploadItemImage);
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));
                            isUploaded = true;
                            message = "File uploaded successfully!";
                            itemInfor.ItemImageURL = "/" + file.FileName;
                            TempData["HttpPostedFileBase"] = null;
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                }

                bool ret = adminPageProvider.EditItem(itemInfor);
                if (ret)
                {
                    return LoadAllItem(allStatus, sPageIndex);
                }
                else
                {
                    jResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult DeleteItem(int deleteItemID)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                bool ret = adminPageProvider.DeleteItem(deleteItemID);
                if (ret)
                {
                    return LoadAllItem(allStatus, 0);
                }
                else
                {
                    jResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }


            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadAllNews(string sNewsStatus, int sPageIndex)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                totalCount = 0;
                IList<News> lstItem = adminPageProvider.LoadAllNews(sNewsStatus, Var.DefaultValueInComboBox, ref totalCount);
                IPagedList<News> lstReturn = lstItem.ToPagedList(sPageIndex, Var.PageSize, totalCount);

                jResult = Json(new { success = true, returnList = lstReturn, PageCount = lstReturn.PageCount, HasPreviousPage = lstReturn.HasPreviousPage, HasNextPage = lstReturn.HasNextPage }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult LoadNewsByNewsID(int sNewsID)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.LoadNewsByNewsID(sNewsID) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult AddNewNews(News newsInfor)
        {
            JsonResult jResult = new JsonResult();

            try
            {
                bool isUploaded = false;
                string message = "File upload failed";
                if (TempData["HttpPostedFileBase"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase"];
                    string pathForSaving = Server.MapPath(Var.UrlUploadItemImage);
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));
                            isUploaded = true;
                            message = "File uploaded successfully!";
                            newsInfor.ItemImageURL = "/" + file.FileName;
                            TempData["HttpPostedFileBase"] = null;
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                }

                bool ret = adminPageProvider.AddNewNews(newsInfor);
                if (ret)
                {
                    return LoadAllNews(allStatus, 0);
                }
                else
                {
                    jResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                 
            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult EditNews(News newsInfor)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                bool isUploaded = false;
                string message = "File upload failed";
                if (TempData["HttpPostedFileBase"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase"];
                    string pathForSaving = Server.MapPath(Var.UrlUploadCompanyNewsImage);
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));
                            isUploaded = true;
                            message = "File uploaded successfully!";
                            newsInfor.ItemImageURL = "/" + file.FileName;
                            TempData["HttpPostedFileBase"] = null;
                        }
                        catch (Exception ex)
                        {
                            message = string.Format("File upload failed: {0}", ex.Message);
                        }
                    }
                }

                bool ret = adminPageProvider.EditNews(newsInfor);
                if (ret)
                {
                    return LoadAllNews(allStatus, 0);
                }
                else
                {
                    jResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                 
            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult DeleteNews(int deleteNewsID)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                bool ret = adminPageProvider.DeleteNews(deleteNewsID);
                if (ret)
                {
                    return LoadAllNews(allStatus, 0);
                }
                else
                {
                    jResult = Json(new { success = false }, JsonRequestBehavior.AllowGet);
                }
                 

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult UploadFile(HttpPostedFileBase file)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                if (file != null && file.ContentLength != 0)
                {
                    TempData["HttpPostedFileBase"] = file;

                }

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        private bool CreateFolderIfNeeded(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception)
                {
                    /*TODO: You must process this exception.*/
                    result = false;
                }
            }
            return result;
        }

        public JsonResult GetSysPara(string fieldSysPara)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.GetSysPara(fieldSysPara) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult EditSysPara(SysPara editSysPara)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = adminPageProvider.EditSysPara(editSysPara) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }
    }
}