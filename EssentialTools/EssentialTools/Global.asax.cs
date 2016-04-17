using System.Web.Mvc;
using System.Web.Routing;
using EssentialTools.Infrastructure;

namespace EssentialTools
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            DependencyResolver.SetResolver(new NinjectDependencyResolver());
            
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
