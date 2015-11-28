using EntitySolution.Domain.Common;
using EntitySolution.WebUI.Infrastructure;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing; 

namespace EntitySolution.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        private void Application_AcquireRequestState(Object source, EventArgs e)
        {
            try
            {
                //It's important to check whether session object is ready
                if (HttpContext.Current.Session != null)
                {

                    CultureInfo ci = (CultureInfo)this.Session["Culture"];
                     
                    if (ci == null)
                    {

                        ci = new CultureInfo(Var.CultureLanguage);

                        this.Session["Culture"] = ci;
                        
                    }

                    //Finally setting culture for each request
                    Thread.CurrentThread.CurrentUICulture = ci;
                    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(ci.Name);
                }
            }

            catch (Exception ex)
            {

                //ErrorHandle.WriteError(ex.ToString());
            }
        }

    }
}
