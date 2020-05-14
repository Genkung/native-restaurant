using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Restaurant.iOS;
using Restaurant.Views;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ContentPage), typeof(ContentPageRenderer))]
namespace Restaurant.iOS
{
    public class ContentPageRenderer : PageRenderer
    {
        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);

            if (Element is WebViewPage)
            {
                SetCustomBackButton();
            }
        }
        private void SetCustomBackButton()
        {
            var backBtnImage = UIImage.FromBundle("iosbackarrow.png");

            backBtnImage = backBtnImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);

            var backButtonItemWithImage = new UIBarButtonItem(backBtnImage, UIBarButtonItemStyle.Plain, (s, e) =>
            {
                var page = (ContentPage)Element as WebViewPage;
                page.GoBack();
            })
            {
                ImageInsets = new UIEdgeInsets(0f, -8f, 0f, 0f)
            };

            UIBarButtonItem[] bArray = { backButtonItemWithImage };

            NavigationController.TopViewController.NavigationItem.SetLeftBarButtonItems(bArray, false);
        }
    }
}