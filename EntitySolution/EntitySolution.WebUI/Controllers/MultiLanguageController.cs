using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntitySolution.WebUI.Controllers
{
    public class MultiLanguageController : Controller
    {
        //
        // GET: /MultiLanguage/
        public PartialViewResult MultiLanguageNav()
        {

            return PartialView();
        }

        public ActionResult SwitchLang(string lang, string returnUrl)
        {
            Session["Culture"] = new CultureInfo(lang);
            return Redirect(returnUrl);
        }
	}
}