using EntitySolution.Domain.Abstract;
using EntitySolution.EntityDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntitySolution.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IAdminPageRepository categoryProvider;

        public HomeController(IAdminPageRepository categoryRepository)
        {
            categoryProvider = categoryRepository;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult GetAllCategory()
        {
            JsonResult jResult = new JsonResult();
            try
            {
                List<Category> lstData = categoryProvider.GetAllCategory();
                jResult = Json(lstData, JsonRequestBehavior.AllowGet);

            }
            catch (Exception)
            {

                throw;
            }

            return jResult;
        }
    }
}