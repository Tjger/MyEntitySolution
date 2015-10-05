using EntitySolution.Domain.Abstract;
using EntitySolution.Domain.Concrete;
using Ninject;
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace EntitySolution.WebUI.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;
        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null ? null : (IController)ninjectKernel.Get(controllerType);
        }

        private void AddBindings()
        {
            // put bindings here 
            ninjectKernel.Bind<ICategoryRepository>().To<EFCategoryRepository>();
            ninjectKernel.Bind<IAuthenticateRepository>().To<EFAuthenticateRepository>();
        }
    }
}