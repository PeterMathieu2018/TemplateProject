using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TemplateProject.UI.Startup))]
namespace TemplateProject.UI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
