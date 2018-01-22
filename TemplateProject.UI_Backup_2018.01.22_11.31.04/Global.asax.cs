using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using TemplateProject.UI.App_Start;
using TemplateProject.UI.Data.MDDB;
using TemplateProject.UI.Data.ProlactaCentral;
using TemplateProject.UI.Data.RMA;
using TemplateProject.UI.Dto;
using TemplateProject.UI.Dto.MDDB;
using TemplateProject.UI.Dto.ProlactaCentral;
using TemplateProject.UI.Dto.RMA;
using Donor = TemplateProject.UI.Data.RMA.Donor;

namespace TemplateProject.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Mapper.Initialize(cfg =>
            {
                //Create Mappings here;
                //example cfg.CreateMap<Entity,EntityDto>();
            });
        }
    }
}
