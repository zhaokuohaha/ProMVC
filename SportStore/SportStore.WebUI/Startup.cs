using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportStore.WebUI.Startup))]
namespace SportStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
