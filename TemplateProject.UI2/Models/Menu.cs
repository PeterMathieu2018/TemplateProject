using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateProject.UI2.Models
{
    public class Menu
    {
        public List<MenuItem> MenuItems { get; set; }

        public Menu()
        {
            MenuItems = new List<MenuItem>
            {
                new MenuItem
                {
                    Id = 100,
                    NameOption = "Menu Item",
                    Controller = "Home",
                    Action = "Index",
                    ImageClass = "fa fa-users fa-lg",
                    Status = true,
                    IsParent = true,
                    ParentId = 0,
                    RequiredRoles = System.Configuration.ConfigurationManager.AppSettings["OrdersAccess"],
                    SubMenu= new List<MenuItem>()
                    {
                        new MenuItem()
                        {
                            Id = 100,
                            NameOption = "Sub Menu",
                            Controller = "Home",
                            Action = "Index",
                            ImageClass = "fa fa-address-card",
                            Status = true,
                            IsParent = false,
                            ParentId = 0
                        }
                    }
                }
            };
        }
    }
}