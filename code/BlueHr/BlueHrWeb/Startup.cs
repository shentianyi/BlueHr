using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BlueHrWeb.Startup))]
namespace BlueHrWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
