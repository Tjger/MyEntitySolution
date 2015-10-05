using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using EntitySolution.WebUI.Models;
using EntitySolution.Domain.Common;
using EntitySolution.Domain.Abstract;

namespace EntitySolution.WebUI.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private IAuthenticateRepository authenticateRepository;
        public AccountController(IAuthenticateRepository authenticateProvider)
        {
            authenticateRepository = authenticateProvider;
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
               
                if (model != null)
                {
                    if (model.Password != "" && model.UserName != "")
                    {
                       
                        if (authenticateRepository.Authenticate(model.UserName, model.Password))
                        {
                            return RedirectToAction("Receipt", "Account");
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

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Receipt()
        {
            return View();
        }
    }
}