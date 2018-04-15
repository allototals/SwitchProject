using Microsoft.Owin;
using Owin;
using System.Configuration;
using System.Web.Mvc;

[assembly: OwinStartupAttribute(typeof(SwitchProject.Startup))]
namespace SwitchProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
          // GlobalConfiguration.
           // Switch.Service.ServiceBootrapper.Initialise();
           // HttpConfiguration config = new HttpConfiguration();

           // var container = Switch.Service.ServiceBootrapper.Initialise();
            //DependencyResolver.SetResolver(new Unity.Mvc3.UnityDependencyResolver(container));
            ConfigureAuth(app);
        }
    }
}
