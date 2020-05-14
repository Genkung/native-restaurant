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

            GotoMain.Clicked += (s, e) =>
            {
                var homePage = new NavigationPage(new MainPage());
                var masterDetailPage = new MasterDetailPage
                {
                    Detail = homePage,
                    IsGestureEnabled = false
                };

                masterDetailPage.Master = new MenuPage();
                masterDetailPage.Master.IconImageSource = "hammenu";

                App.Current.MainPage = masterDetailPage;
            };
        }
    }
}