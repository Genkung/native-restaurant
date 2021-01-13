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
    public partial class WebViewPage : WebviewBase
    {
        public WebViewPage(string pageName, object parameter)
        {
            InitializeComponent();
            AddXWebview(wWebview);
            InitWebview(pageName, parameter);
            RegisterWebviewBaseFunction(wWebview);
        }

        private void InitWebview(string pageName, object parameter)
        {
            wWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(WebviewService.MCLocalStorageFolderName));

            var htmlSource = WebviewService.GetHtmlPathByName(pageName);
            wWebview.Source = $"{htmlSource}{WebviewService.ConvertObjectToUrlParameters(parameter)}";
        }

        public override async Task<object[]> NavigateToPage(string param)
        {
            return new object[] { false };
        }

        public override async void Goback(string param)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                XamarinGoBack();
            });
        }

        public override async void PopToRoot(string param)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PopAsync(true);
            });
        }

        public async void XamarinGoBack()
        {
            wWebview.RefreshCanGoBackForward();
            if (wWebview.CanGoBack)
            {
                wWebview.GoBack();
            }
            else
            {
                await Navigation.PopAsync(true);
            }
        }
    }
}