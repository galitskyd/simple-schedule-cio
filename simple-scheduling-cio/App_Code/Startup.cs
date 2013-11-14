using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(simple_scheduling_cio.Startup))]
namespace simple_scheduling_cio
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
