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
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage(string pageName, object parameters)
        {
            InitializeComponent();

            myWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(WebviewService.MCLocalStorageFolderName));

            var htmlSource = WebviewService.GetHtmlPathByName(pageName);

            RegisterWebviewBaseFunction();

            myWebview.Source = $"{htmlSource}{WebviewService.ConvertObjectToUrlParameters(parameters)}";
        }

        private void RegisterWebviewBaseFunction()
        {
            myWebview.RegisterNativeFunction("NavigateToPage", NavigateToPage);
            myWebview.RegisterNativeFunction("GetRestaurantId", GetRestaurantId);
            myWebview.RegisterCallback("Goback", Goback);
            myWebview.RegisterCallback("PopToRoot", PopToRoot);
            myWebview.RegisterCallback("SetPageTitle", SetPageTitle);
            myWebview.RegisterCallback("ExecuteNotiIfExist", ExecuteNotiIfExist);
            myWebview.RegisterCallback("RemoveNotificationChannel", RemoveNotificationChannel);
            myWebview.RegisterCallback("PhoneCall", PhoneCall);
            myWebview.RegisterCallback("UpdateSidemenuItem", UpdateSidemenuItem);
        }

        private async Task<object[]> NavigateToPage(string param)
        {
            return new object[] { false };
        }

        private async void Goback(string param)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                GoBack();
            });
        }

        private async void PopToRoot(string param)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync(true);
            });
        }

        private async Task<object[]> GetRestaurantId(string param)
        {
            var restaurant = RestaurantService.GetRestaurantInfo();
            return new object[] { restaurant._id };
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

        private async void PhoneCall(string phoneNumber)
        {
            PhoneService.Call(phoneNumber);
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
                    var msg = $"onSendNotification('{obj.NotiChannel}',{obj.Params});";
                    await myWebview?.EvaluateJavaScriptAsync(msg);
                });
            });

            myWebview.EvaluateJavaScriptAsync("refreshOnGoBack();");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NotificationService.UnSubscriptNotification();
        }

        public async void GoBack()
        {
            myWebview.RefreshCanGoBackForward();
            if (myWebview.CanGoBack)
            {
                myWebview.GoBack();
            }
            else
            {
                await Navigation.PopAsync(true);
            }
        }
    }
}