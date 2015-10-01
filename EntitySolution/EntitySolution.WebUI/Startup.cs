using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EntitySolution.WebUI.Startup))]
namespace EntitySolution.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
