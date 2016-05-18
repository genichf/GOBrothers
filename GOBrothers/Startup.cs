using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GOBrothers.Startup))]
namespace GOBrothers
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
