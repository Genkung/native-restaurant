using Restaurant.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public class PageService
    {
        public static MainPage GetRootPage()
        {
            return ((NavigationPage)((MasterDetailPage)Application.Current.MainPage).Detail).CurrentPage as MainPage;
        }
        public static Page GetCurrentPage()
        {
            var mainPage = Application.Current.MainPage;
            if (mainPage is MasterDetailPage)
            {
                return ((NavigationPage)
                       ((MasterDetailPage)mainPage).Detail).CurrentPage;
            }
            else if (mainPage is NavigationPage)
            {
                return ((NavigationPage)mainPage).CurrentPage;
            }
            else
            {
                return mainPage;
            }
        }
    }
}
