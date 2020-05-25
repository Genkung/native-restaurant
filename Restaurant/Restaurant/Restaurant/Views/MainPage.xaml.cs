using Newtonsoft.Json;
using Restaurant.Models;
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
            SetPage();
        }

        private async void SetPage()
        {
            myWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(WebviewService.MCLocalStorageFolderName));
            var htmlSource = WebviewService.GetHtmlPathByName("order-main");

            myWebview.NavigateOrRequesting += (s, e) =>
            {
                MessagingCenter.Send(this, MessagingChannel.HomeReady, string.Empty);
            };

            RegisterWebviewBaseFunction();

            myWebview.Source = htmlSource;
        }

        private void RegisterWebviewBaseFunction()
        {
            myWebview.RegisterNativeFunction("NavigateToPage", NavigateToPage);
            myWebview.RegisterNativeFunction("GetBikerId", GetBikerId);
            myWebview.RegisterCallback("Goback", Goback);
            myWebview.RegisterCallback("PopToRoot", PopToRoot);
            myWebview.RegisterCallback("SetPageTitle", SetPageTitle);
            myWebview.RegisterCallback("ExecuteNotiIfExist", ExecuteNotiIfExist);
            myWebview.RegisterCallback("RemoveNotificationChannel", RemoveNotificationChannel);
            myWebview.RegisterCallback("UpdateSidemenuItem", UpdateSidemenuItem);
        }

        private async Task<object[]> NavigateToPage(string param)
        {
            var paramObject = JsonConvert.DeserializeObject<NavigateToPageParameter>(param);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new WebViewPage(paramObject.PageName, paramObject.Params));
            });
            return new object[] { true };
        }

        private async Task<object[]> GetBikerId(string param)
        {
            var restaurant = RestaurantService.GetRestaurantInfo();
            return new object[] { restaurant._id };
        }

        private async void Goback(string param)
        {
        }

        private async void PopToRoot(string param)
        {
        }

        private async void SetPageTitle(string title)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                Title = title;
            });
        }

        private async void ExecuteNotiIfExist(string notiChannel)
        {
            NotificationService.ExecuteNotificationIfExist(notiChannel);
        }

        private async void RemoveNotificationChannel(string notiChannel)
        {
            NotificationService.RemoveNotificationStack(notiChannel);
        }

        private async void UpdateSidemenuItem(string param)
        {
            var sidemenu = JsonConvert.DeserializeObject<SideMenuItem>(param);
            SidemenuService.UpdateSidemenuPage(sidemenu.Title, sidemenu.Page, sidemenu.Params);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            myWebview.Focus();

            NotificationService.SubscriptNotification((sender, obj) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await myWebview?.EvaluateJavaScriptAsync($"onSendNotification('{obj.NotiChannel}',{obj.Params});");
                });
            });

            myWebview.EvaluateJavaScriptAsync("refreshOnGoBack();");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NotificationService.UnSubscriptNotification();
        }

        public void SideMenuChangePage(string page, object parameters)
        {
            var htmlSource = WebviewService.GetHtmlPathByName(page);
            myWebview.Source = $"{htmlSource}{WebviewService.ConvertObjectToUrlParameters(parameters)}";
        }
    }
}