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
using Newtonsoft.Json.Linq;
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
                        string sEmail = "";
                        if (authenticateProvider.Authenticate(model.UserName, model.Password, ref isSuperAdmin, ref sUserID, ref sEmail))
                        {
                            Session["EmpID"] = sUserID;
                            Session["LoginID"] = model.UserName;
                            Session["EmpEmail"] = sEmail;
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
            ModelState.AddModelError("", "Invalid username or password.");
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
                ViewBag.LoginID = Session["LoginID"];
                ViewBag.EmpEmail = Session["EmpEmail"];
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

        //[AcceptVerbs(HttpVerbs.Post)]
        //public JsonResult UpdateSysParam(string lstSysparam)
        //{
        //    JsonResult jsonResult = new JsonResult();
        //    HttpRequestBase request = this.HttpContext.Request;
        //    if (ValidateRequestHeader(request))
        //    {
        //        try
        //        {
        //            if (((List<int>)Session["UserRight"]).Contains((int)Var.UserRightEnum.Edit_System_Parameter))
        //            {

        //                var arrItem = JArray.Parse(lstSysparam);
        //                List<SysParaEntity> lst = new List<SysParaEntity>();
        //                foreach (var item in arrItem)
        //                {
        //                    SysParaEntity oSysPara = new SysParaEntity();
        //                    oSysPara.Module = item["Module"].ToString();
        //                    oSysPara.Field = item["Field"].ToString();
        //                    oSysPara.Value = item["Value"] == null ? "" : item["Value"].ToString();
        //                    if (item["CommonVariable"] != null && item["CommonVariable"].ToString() != "" && item["CommonVariable"].ToString() == "1")
        //                    {
        //                        oSysPara.RevID = "";
        //                        oSysPara.SiteID = "";
        //                    }
        //                    else
        //                    {
        //                        oSysPara.RevID = Session["RevID"].ToString();
        //                        oSysPara.SiteID = Session["SiteID"].ToString();
        //                    }

        //                    lst.Add(oSysPara);
        //                }

        //                string Mes = string.Empty;

        //                if (lst != null && lst.Count > 0)
        //                {
        //                    if (_systemVariableProvider.UpdateListSystemPara(lst, ref Mes))
        //                    {

        //                        jsonResult = Json(new { success = true, msgError = Var.None_Error, errorCode = ((int)Var.eErrorCodePage.None_Error).ToString() }, JsonRequestBehavior.AllowGet);
        //                    }
        //                    else
        //                    {
        //                        jsonResult = Json(new { success = false, msgError = Var.Unknown_Error, errorCode = ((int)Var.eErrorCodePage.Unknown_Error).ToString() }, JsonRequestBehavior.AllowGet);
        //                    }
        //                }
        //                else
        //                {
        //                    jsonResult = Json(new { success = false, msgError = Var.Unknown_Error, errorCode = ((int)Var.eErrorCodePage.Unknown_Error).ToString() }, JsonRequestBehavior.AllowGet);
        //                }


        //            }
        //            else
        //            {
        //                jsonResult = Json(new { success = false, msgError = Var.Right_Authentication_Error, errorCode = ((int)Var.eErrorCodePage.Right_Authentication_Error).ToString() }, JsonRequestBehavior.AllowGet);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            jsonResult = Json(new { success = false, msgError = Var.Unknown_Error, errorCode = ((int)Var.eErrorCodePage.Unknown_Error).ToString() }, JsonRequestBehavior.AllowGet);
        //            ErrorHandle.WriteError(ex.Message);
        //        }
        //    }
        //    else
        //    {
        //        jsonResult = Json(new { success = false, msgError = Var.Authentication_Error, errorCode = ((int)Var.eErrorCodePage.Authentication_Error).ToString() }, JsonRequestBehavior.AllowGet);

        //    }
        //    return jsonResult;

        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public JsonResult UpdateSysParam(string lstSysparam)
        {
            string Mes = string.Empty;
            JsonResult jsonResult = new JsonResult();
            try
            {
                if (Session["LoginID"] != null)
                {

                    var arrItem = JArray.Parse(lstSysparam);
                    List<SysPara> lst = new List<SysPara>();
                    foreach (var item in arrItem)
                    {
                        SysPara oSysPara = new SysPara();
                         
                        oSysPara.Field = item["Field"].ToString();
                        oSysPara.Value = item["Value"] == null ? "" : item["Value"].ToString();
                        oSysPara.DefaultValue = item["DefaultValue"] == null ? "" : item["DefaultValue"].ToString();
                        adminPageProvider.EditSysPara(oSysPara);
                    }
                    adminPageProvider.SetSysPara();
                    jsonResult = Json(new { success = true, msgError = Mes }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    jsonResult = Json(new { success = false, msgError = Mes }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                jsonResult = Json(new { success = false, msgError = Mes }, JsonRequestBehavior.AllowGet);
             
            }
            return jsonResult;

        }

        public JsonResult ChangePassword(string NewPassword, string NewConfirmPassword)
        {
            JsonResult jResult = new JsonResult();
            try
            {
                if (Session["LoginID"] != null)
                {
                    if (NewPassword != null && NewPassword != "" && NewPassword == NewConfirmPassword)
                    {
                        jResult = Json(new { success = authenticateProvider.ChangePassword(Session["LoginID"].ToString(), NewPassword), MsgError = "Có Lỗi Xảy Ra, Hãy Thử Lại Sau" }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        jResult = Json(new { success = false, MsgError = "Mật Khẩu Và Xác Nhận Mật Khẩu Phải Giống Nhau" }, JsonRequestBehavior.AllowGet);
                    }
                    
                }
                else
                {
                    jResult = Json(new { success = false, MsgError = "Session Đã Quá Hạn, Làm Ơn Đăng Nhập Và Thử Lại Sau" }, JsonRequestBehavior.AllowGet);
                }
                 
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
               
                string message = "File upload failed";
                string idFolder = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                string pathForSaving = Server.MapPath(Var.UrlUploadItemImage + "/" + idFolder );

                SetURLImage(pathForSaving, idFolder, ref itemInfor);

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
               
                string message = "File upload failed";
                string pathForSaving = Server.MapPath(Var.UrlUploadItemImage + "/" + itemInfor.FolderID);
               
                SetURLImage(pathForSaving, itemInfor.FolderID, ref itemInfor);
                 
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

        public JsonResult UploadFile(HttpPostedFileBase file )
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

        public void SetURLImage(string pathForSaving,string  idFolder, ref Item itemInfor)
        {
            try
            {

                itemInfor.FolderID = idFolder;
                if (TempData["HttpPostedFileBase"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase"];
                  
                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));
                            
                             
                            itemInfor.ItemImageURL = "/" + idFolder + "/" + file.FileName;
                            TempData["HttpPostedFileBase"] = null;
                        }
                        catch (Exception ex)
                        {
                           
                        }
                    }
                }

                if (TempData["HttpPostedFileBase2"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase2"];

                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));

                          
                            itemInfor.ItemImageURL2 = "/" + idFolder + "/" + file.FileName;
                            TempData["HttpPostedFileBase2"] = null;
                        }
                        catch (Exception ex)
                        {
                            
                        }
                    }
                }

                if (TempData["HttpPostedFileBase3"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase3"];

                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));


                            itemInfor.ItemImageURL3 = "/" + idFolder + "/" + file.FileName;
                            TempData["HttpPostedFileBase3"] = null;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }
                if (TempData["HttpPostedFileBase4"] != null)
                {

                    HttpPostedFileBase file = (HttpPostedFileBase)TempData["HttpPostedFileBase4"];

                    if (this.CreateFolderIfNeeded(pathForSaving))
                    {
                        try
                        {
                            file.SaveAs(Path.Combine(pathForSaving, file.FileName));


                            itemInfor.ItemImageURL4 = "/" + idFolder + "/" + file.FileName;
                            TempData["HttpPostedFileBase4"] = null;
                        }
                        catch (Exception ex)
                        {

                        }
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public JsonResult UploadFile2(HttpPostedFileBase file)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                if (file != null && file.ContentLength != 0)
                {
                    TempData["HttpPostedFileBase2"] = file;

                }

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult UploadFile3(HttpPostedFileBase file)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                if (file != null && file.ContentLength != 0)
                {
                    TempData["HttpPostedFileBase3"] = file;

                }

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult UploadFile4(HttpPostedFileBase file)
        {
            JsonResult jResult = new JsonResult();
            try
            {

                if (file != null && file.ContentLength != 0)
                {
                    TempData["HttpPostedFileBase4"] = file;

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