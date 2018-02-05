using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ABS_v3.Startup))]
namespace ABS_v3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
