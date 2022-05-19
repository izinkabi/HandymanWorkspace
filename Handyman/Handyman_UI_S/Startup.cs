using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Handyman_UI_S.Startup))]

namespace Handyman_UI_S
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
