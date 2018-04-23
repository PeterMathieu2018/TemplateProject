using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TemplateProject.UI2.Startup))]
namespace TemplateProject.UI2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
