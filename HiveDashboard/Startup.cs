using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HiveDashboard.Startup))]
namespace HiveDashboard
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
