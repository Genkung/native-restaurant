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
        private const string MCLocalStorageFolderName = "mcontent/ionicapp4";
        private readonly string destinationFolder;
        public WebViewPage(string pageName)
        {
            InitializeComponent();

            var dirPath = Environment.SpecialFolder.LocalApplicationData;
            var defaultDirPath = Environment.GetFolderPath(dirPath);
            destinationFolder = Path.Combine(defaultDirPath, MCLocalStorageFolderName);

            myWebview.Accessors = new TheS.DevXP.XamForms.XWebViewAccessorCollection(
                LocalContentAccessor.GetAppData(MCLocalStorageFolderName));
            var htmlSource = $"{GetMContentBaseUrl()}index.html#/{pageName}";
            myWebview.Source = htmlSource;
        }

        public string GetMContentBaseUrl()
        {
            return string.Format("file://{0}/", destinationFolder);
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