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
using TheS.DevXP.XamForms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : WebviewBase
    {
        public MainPage()
        {
            InitializeComponent();
            AddXWebview(mWebview);
            InitWebview();
            RegisterWebviewBaseFunction(mWebview);
        }

        private void InitWebview()
        {
            mWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(WebviewService.MCLocalStorageFolderName));
            var htmlSource = WebviewService.GetHtmlPathByName("order-main");

            mWebview.NavigateOrRequesting += (s, e) =>
            {
                MessagingCenter.Send(this, MessagingChannel.HomeReady, string.Empty);
            };

            mWebview.Source = htmlSource;
        }

        public override async Task<object[]> NavigateToPage(string param)
        {
            var paramObject = JsonConvert.DeserializeObject<NavigateToPageParameter>(param);
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new WebViewPage(paramObject.PageName, paramObject.Params));
            });
            return new object[] { true };
        }

        public override async void Goback(string param)
        {
        }

        public override async void PopToRoot(string param)
        {
        }

        public void SideMenuChangePage(string page, object parameters)
        {
            var htmlSource = WebviewService.GetHtmlPathByName(page);
            mWebview.Source = $"{htmlSource}{WebviewService.ConvertObjectToUrlParameters(parameters)}";
        }
    }
}