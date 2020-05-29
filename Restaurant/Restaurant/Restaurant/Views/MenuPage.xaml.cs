using Restaurant.Models;
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
    public partial class MenuPage : ContentPage
    {
        public MenuPage()
        {
            InitializeComponent();

            var restaurantProfile = RestaurantService.GetRestaurantInfo();

            restaurantProfileImage.Source = restaurantProfile.LogoImage;
            restaurantProfileName.Text = restaurantProfile.Name;

            var menuList = SidemenuService.GetSidemenuItem();

            ListViewMenu.ItemsSource = menuList;
            ListViewMenu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;

                var page = ((SideMenuItem)e.SelectedItem).Page;
                var parameters = ((SideMenuItem)e.SelectedItem).Params;
                PageService.GetRootPage().SideMenuChangePage(page, parameters);
                ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
                ListViewMenu.SelectedItem = null;
            };
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Logout();
        }

        private async Task Logout()
        {
            await NotificationService.UnRegisterDevice();
            App.Current.MainPage = new LoginPage();
        }
    }
}