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
using System.IO;
namespace EntitySolution.WebUI.Controllers
{
    public class AdminPageController : Controller
    {
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


        public JsonResult LoadAllItem(string sItemStatus)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.LoadAllItem(sItemStatus) }, JsonRequestBehavior.AllowGet);

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


                jResult = Json(new { success = adminPageProvider.AddNewItem(itemInfor), returnList = adminPageProvider.LoadAllItem(allStatus), isUploaded = isUploaded, msgError = message }, JsonRequestBehavior.AllowGet);

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

        public JsonResult EditItem(Item itemInfor)
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

                jResult = Json(new { success = adminPageProvider.EditItem(itemInfor), returnList = adminPageProvider.LoadAllItem(allStatus), isUploaded = isUploaded, msgError = message }, JsonRequestBehavior.AllowGet);

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

                jResult = Json(new { success = adminPageProvider.DeleteItem(deleteItemID), returnList = adminPageProvider.LoadAllItem(allStatus) }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }
    }
}