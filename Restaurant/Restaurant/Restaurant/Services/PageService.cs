using Restaurant.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
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

        public static async Task<string> DisplayActionSheet(string title, string cancel, string destruction, params string[] buttons)
        {
            return await GetCurrentPage().DisplayActionSheet(title, cancel, destruction, buttons);
        }

        public static async Task DisplayAlert(string title, string message, string cancel)
        {
            await GetCurrentPage().DisplayAlert(title, message, cancel);
        }

        public async Task<bool> DisplayAlert(string title, string message, string accept, string cancel)
        {
            return await GetCurrentPage().DisplayAlert(title, message, accept, cancel);
        }
    }
}
