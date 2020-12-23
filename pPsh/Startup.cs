using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(pPsh.Startup))]
namespace pPsh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
