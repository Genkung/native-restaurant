using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restaurant.Services
{
    public class SidemenuService
    {
        private static List<SideMenuItem> menuList = new List<SideMenuItem>();

        public static void SetUpSideMenu()
        {
            menuList = new List<SideMenuItem>
            {
                new SideMenuItem{ Title = SideMenuPageTitle.HomePage, Page = "order-main" },
                new SideMenuItem{ Title = SideMenuPageTitle.History, Page = "history-main" },
                new SideMenuItem{ Title = SideMenuPageTitle.Menu, Page = "menu-main" },
                new SideMenuItem{ Title = SideMenuPageTitle.Contract, Page = "contract-main" },
                new SideMenuItem{ Title = SideMenuPageTitle.Settings, Page = "setting-main" }
            };
        }

        public static void UpdateSidemenuPage(string title, string page, object param = null)
        {
            menuList.FirstOrDefault(it => it.Title == title).Page = page;
            if (param != null)
            {
                menuList.FirstOrDefault(it => it.Title == title).Params = param;
            }
        }

        public static List<SideMenuItem> GetSidemenuItem()
        {
            return menuList;
        }
    }
}
