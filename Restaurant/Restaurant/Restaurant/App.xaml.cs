using System;
using Restaurant.Views;
using System.IO;
using System.IO.Compression;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Restaurant
{
    public partial class App : Application
    {
        private const string MCLocalStorageFolderName = "mcontent";
        private readonly string destinationFolder;

        public App()
        {
            InitializeComponent();

            var dirPath = Environment.SpecialFolder.LocalApplicationData;
            var defaultDirPath = Environment.GetFolderPath(dirPath);
            destinationFolder = Path.Combine(defaultDirPath, MCLocalStorageFolderName);
            DownloadZip("https://manadevfrom.blob.core.windows.net/zips/html20201205085302.zip");

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        private void DownloadZip(string DownloadFileURL)
        {

            if (Directory.Exists($"{destinationFolder}"))
            {
                Directory.Delete($"{destinationFolder}", true);
            }
            var data = new WebClient().DownloadData(new Uri(DownloadFileURL));
            ExtractZip(data);
        }

        private void ExtractZip(byte[] data)
        {
            using (var stream = new MemoryStream(data))
            using (var archive = new ZipArchive(stream))
            {
                archive.ExtractToDirectory(destinationFolder);
            }
        }
    }
}
