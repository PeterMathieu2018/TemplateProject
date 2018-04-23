using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UIProject21.Startup))]
namespace UIProject21
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
