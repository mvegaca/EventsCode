using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LordOfCodes.Web.Startup))]
namespace LordOfCodes.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
