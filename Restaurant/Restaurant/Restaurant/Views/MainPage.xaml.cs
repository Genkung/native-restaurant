using Restaurant.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheS.DevXP.XamForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            myWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(WebviewService.MCLocalStorageFolderName));
            var htmlSource = WebviewService.GetHtmlPathByName("master");

            myWebview.NavigateOrRequesting += (s, e) =>
            {
                MessagingCenter.Send(this, "HomeReady", string.Empty);
            };

            myWebview.RegisterCallback("Navigate", page =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await Navigation.PushAsync(new WebViewPage(page));
                });
            });

            myWebview.Source = htmlSource;
        }

        public void ChangePage(string page)
        {
            var htmlSource = WebviewService.GetHtmlPathByName(page);
            myWebview.Source = htmlSource;
        }
    }
}