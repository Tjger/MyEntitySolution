﻿using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using EntitySolution.WebUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EntitySolution.WebUI.Controllers
{
    public class AdminPageController : Controller
    {
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


        public JsonResult LoadAllCategory()
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.GetAllCategory() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult SaveCategory()
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.GetAllCategory() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }

        public JsonResult EditCategory()
        {
            JsonResult jResult = new JsonResult();
            try
            {

                jResult = Json(new { success = true, returnList = adminPageProvider.GetAllCategory() }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {


            }

            return jResult;
        }
    }
}