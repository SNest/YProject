using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YFit.Web.Startup))]
namespace YFit.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
