using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateProject.UI2.Models;

namespace TemplateProject.UI2.Controllers
{
    [ChildActionOnly]
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Index()
        {
            return PartialView("_Menu", new Menu());
        }
    }
}