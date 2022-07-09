using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Handyman_UI_Web.Startup))]

namespace Handyman_UI_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
