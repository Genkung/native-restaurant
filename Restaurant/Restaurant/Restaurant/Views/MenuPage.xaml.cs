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

            var menuList = new List<SideMenuItem>
            {
                new SideMenuItem{ Title = "HomePage", Page = "master" },
                new SideMenuItem{ Title = "A2", Page = "a2" }
            };

            ListViewMenu.ItemsSource = menuList;
            ListViewMenu.SelectedItem = menuList[0];
            ListViewMenu.ItemSelected += (sender, e) =>
            {
                if (e.SelectedItem == null) return;

                var page = ((SideMenuItem)e.SelectedItem).Page;
                PageService.GetRootPage().ChangePage(page);
                ((MasterDetailPage)Application.Current.MainPage).IsPresented = false;
            };
        }
    }
}