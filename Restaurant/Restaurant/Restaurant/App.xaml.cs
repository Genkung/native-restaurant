using System;
using Restaurant.Views;
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.Generic;
using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Xamarin.Forms.Internals;
using Restaurant.Services;

namespace Restaurant
{
    public partial class App : Application
    {
        public static bool IsInForeground;
        private const string MCLocalStorageFolderName = "mcontent";
        private readonly string destinationFolder;

        public App()
        {
            InitializeComponent();

            var dirPath = Environment.SpecialFolder.LocalApplicationData;
            var defaultDirPath = Environment.GetFolderPath(dirPath);
            destinationFolder = Path.Combine(defaultDirPath, MCLocalStorageFolderName);
            DownloadZip("https://manadevfrom.blob.core.windows.net/zips/zip-restaurant.zip");

            MainPage = new LoginPage();
        }

        protected override void OnStart()
        {
            IsInForeground = true;
            OneSignal.Current.StartInit("ffd9cbcb-eaf6-4b8e-a767-b2026b6a61ac")//TODO: insert ID here
                .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.None)
                .HandleNotificationReceived(HandleNotificationReceived)
                .HandleNotificationOpened(HandleNotificationOpened)
                .EndInit();
        }

        protected override void OnSleep()
        {
            IsInForeground = false;
        }

        protected override void OnResume()
        {
            IsInForeground = true;
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

        private void HandleNotificationReceived(OSNotification result)
        {
            if (IsInForeground) ProcessNotification(result.payload.additionalData);
        }
        private void HandleNotificationOpened(OSNotificationOpenedResult result)
        {
            ProcessNotification(result.notification.payload.additionalData);
        }

        public void ProcessNotification(IDictionary<string, object> notificationData)
        {
            var isValidNotiMessage = notificationData != null && notificationData.Any();
            if (isValidNotiMessage)
            {
                notificationData.ForEach(it =>
                {
                    NotificationService.AddNotificationStack(it.Key, it.Value);
                    NotificationService.PublishNotification(it.Key);
                });
            }
        }
    }
}
