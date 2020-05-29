using Restaurant.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogingQRAndLinkPage : ContentPage
    {
        public LogingQRAndLinkPage()
        {
            InitializeComponent();

            GotoMain.Clicked += async (s, e) =>
            {
                await RestaurantService.SetRestaurantInfo("1");
                await NotificationService.RegisterDevice();
                NavigateToMasterDetail();
            };
        }

        private void NavigateToMasterDetail()
        {
            SidemenuService.SetUpSideMenu();

            var homePage = new NavigationPage(new MainPage());
            var masterDetailPage = new MasterDetailPage
            {
                Detail = homePage,
                IsGestureEnabled = false
            };

            masterDetailPage.Master = new MenuPage();
            masterDetailPage.Master.IconImageSource = "hammenu";
            App.Current.MainPage = masterDetailPage;
        }
    }
}