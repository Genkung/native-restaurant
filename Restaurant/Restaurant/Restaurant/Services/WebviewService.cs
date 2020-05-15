using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Restaurant.Services
{
    public  class WebviewService
    {
        public static readonly string MCLocalStorageFolderName = "mcontent/ionicapp4";
        public static string GetHtmlPathByName(string pageName)
        {
            var dirPath = Environment.SpecialFolder.LocalApplicationData;
            var defaultDirPath = Environment.GetFolderPath(dirPath);
            var destinationFolder = Path.Combine(defaultDirPath, MCLocalStorageFolderName);
            var MContentBaseUrl = string.Format("file://{0}/", destinationFolder);
            return $"{MContentBaseUrl}index.html#/{pageName}";
        }
    }
}
