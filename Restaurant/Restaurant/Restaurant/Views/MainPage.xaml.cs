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
        private const string MCLocalStorageFolderName = "mcontent/ionicapp4";
        private readonly string destinationFolder;
        public MainPage()
        {
            InitializeComponent();

            var dirPath = Environment.SpecialFolder.LocalApplicationData;
            var defaultDirPath = Environment.GetFolderPath(dirPath);
            destinationFolder = Path.Combine(defaultDirPath, MCLocalStorageFolderName);

            myWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(MCLocalStorageFolderName));
            var htmlSource = $"{GetMContentBaseUrl()}index.html#/master";

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
            var htmlSource = $"{GetMContentBaseUrl()}index.html#/{page}";
            myWebview.Source = htmlSource;
        }

        public string GetMContentBaseUrl()
        {
            return string.Format("file://{0}/", destinationFolder);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WebViewPage("a"));
        }
    }
}