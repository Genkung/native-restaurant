using Com.OneSignal;
using Com.OneSignal.Abstractions;
using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public class NotificationService
    {
        public static NotificationService obj = new NotificationService();
        public static Dictionary<string, string> NotificatiionStack = new Dictionary<string, string>();

        public static void PublishNotification(string notiChannel)
        {
            if (NotificatiionStack.ContainsKey(notiChannel))
            {
                var sendNoti = new PublishNotificationModel
                {
                    NotiChannel = notiChannel,
                    Params = NotificatiionStack.GetValueOrDefault(notiChannel)
                };
                MessagingCenter.Send(obj, MessagingChannel.SendNotification, sendNoti);
            }
        }

        public static async Task RegisterDevice()
        {
            var bikerId = RestaurantService.GetRestaurantInfo()._id;

            var playerId = (await OneSignal.Current.IdsAvailableAsync()).PlayerId;
            var platform = Xamarin.Essentials.DeviceInfo.Platform.ToString().ToLower();

            var deviceInfo = new { InstallationId = playerId, Platform = platform };

            await HttpClientService.Post($"https://delivery-3rd-api.azurewebsites.net/api/Restaurant/RegisterRestaurantDevice/{bikerId}", deviceInfo);
        }

        public static async Task UnRegisterDevice()
        {
            var bikerId = RestaurantService.GetRestaurantInfo()._id;

            var playerId = (await OneSignal.Current.IdsAvailableAsync()).PlayerId;
            var platform = Xamarin.Essentials.DeviceInfo.Platform.ToString().ToLower();

            var deviceInfo = new { installationId = playerId, platform = platform };

            await HttpClientService.Put($"https://delivery-3rd-api.azurewebsites.net/api/Restaurant/UnRegisterRestaurantDevice/{bikerId}", deviceInfo);
        }

        public static void SubscriptNotification(Action<NotificationService, PublishNotificationModel> callback)
        {
            MessagingCenter.Subscribe<NotificationService, PublishNotificationModel>(obj, MessagingChannel.SendNotification, callback);
        }

        public static void UnSubscriptNotification()
        {
            MessagingCenter.Unsubscribe<NotificationService, PublishNotificationModel>(obj, MessagingChannel.SendNotification);
        }

        public static void AddNotificationStack(string notiChannel, string param)
        {
            var aleadyAddNotification = !NotificatiionStack.TryAdd(notiChannel, param);
            if (aleadyAddNotification)
            {
                NotificatiionStack.Remove(notiChannel);
                NotificatiionStack.TryAdd(notiChannel, param);
            }
        }

        public static void RemoveNotificationStack(string notiChannel)
        {
            if (NotificatiionStack.ContainsKey(notiChannel))
            {
                NotificatiionStack.Remove(notiChannel);
            }
        }

        public static void ExecuteNotificationIfExist(string notiChannel)
        {
            string returnParam;
            var notiIsExits = NotificatiionStack.TryGetValue(notiChannel, out returnParam);
            if (notiIsExits)
            {
                PublishNotification(notiChannel);
            }
        }
    }
}
