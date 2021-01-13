using Newtonsoft.Json;
using Restaurant.Models;
using Restaurant.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TheS.DevXP.XamForms.Controls;
using Xamarin.Forms;

namespace Restaurant
{
    public abstract class WebviewBase : ContentPage
    {
        public XWebView xWebview;

        public WebviewBase()
        {
        }

        public void AddXWebview(XWebView xWebview)
        {
            this.xWebview = xWebview;
        }

        public void RegisterWebviewBaseFunction(XWebView xWebview)
        {
            xWebview.RegisterNativeFunction("NavigateToPage", NavigateToPage);
            xWebview.RegisterNativeFunction("GetRestaurantId", GetRestaurantId);
            xWebview.RegisterCallback("Goback", Goback);
            xWebview.RegisterCallback("PopToRoot", PopToRoot);
            xWebview.RegisterCallback("SetPageTitle", SetPageTitle);
            xWebview.RegisterCallback("ExecuteNotiIfExist", ExecuteNotiIfExist);
            xWebview.RegisterCallback("RemoveNotificationChannel", RemoveNotificationChannel);
            xWebview.RegisterCallback("UpdateSidemenuItem", UpdateSidemenuItem);
            xWebview.RegisterCallback("PhoneCall", PhoneCall);
        }

        public abstract Task<object[]> NavigateToPage(string param);

        public abstract void Goback(string param);

        public abstract void PopToRoot(string param);

        private async Task<object[]> GetRestaurantId(string param)
        {
            var restaurant = RestaurantService.GetRestaurantInfo();
            return new object[] { restaurant._id };
        }

        private async void SetPageTitle(string param)
        {
            var titleObj = ConvertParameterFromWebView<SetTitleParam>(param);
            Device.BeginInvokeOnMainThread(() =>
            {
                Title = titleObj?.Title;
            });
        }

        public async void ExecuteNotiIfExist(string param)
        {
            var notiChannelObj = ConvertParameterFromWebView<NotificationParameter>(param);
            NotificationService.ExecuteNotificationIfExist(notiChannelObj?.NotiChannel);
        }

        public async void RemoveNotificationChannel(string param)
        {
            var notiChannelObj = ConvertParameterFromWebView<NotificationParameter>(param);
            NotificationService.RemoveNotificationStack(notiChannelObj?.NotiChannel);
        }

        public async void PhoneCall(string directionParam)
        {
            var phome = ConvertParameterFromWebView<PhoneCallParameter>(directionParam);
            if (phome != null)
            {
                PhoneService.Call(phome?.PhoneNumber);
            }
            else { await App.Current.MainPage.DisplayAlert("แจ้งเตือน", "ขออภัย เกิดข้อผิดพลาด", "ปิด"); }
        }

        private async void UpdateSidemenuItem(string param)
        {
            var sidemenu = JsonConvert.DeserializeObject<SideMenuItem>(param);
            SidemenuService.UpdateSidemenuPage(sidemenu.Title, sidemenu.Page, sidemenu.Params);
        }

        public T ConvertParameterFromWebView<T>(string parameter)
        {
            try
            {
                var param = JsonConvert.DeserializeObject<T>(parameter);
                return param;
            }
            catch (Exception e)
            {
                return default;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            xWebview.Focus();

            NotificationService.SubscriptNotification((sender, obj) =>
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await xWebview?.EvaluateJavaScriptAsync($"onSendNotification('{obj.NotiChannel}',{obj.Params});");
                });
            });

            xWebview.EvaluateJavaScriptAsync("refreshOnGoBack();");
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            NotificationService.UnSubscriptNotification();
        }
    }
}
