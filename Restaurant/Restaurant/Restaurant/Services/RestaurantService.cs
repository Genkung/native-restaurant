using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Services
{
    public class RestaurantService
    {
        private static RestaurantModel restaurantInfo;

        public static async Task SetRestaurantInfo(string userid)
        {
            var restaurantInfo = await HttpClientService.Get<RestaurantModel>($"https://delivery-3rd-api.azurewebsites.net/api/Restaurant/GetRestaurantInfo/{userid}");
            RestaurantService.restaurantInfo = restaurantInfo;
        }

        public static RestaurantModel GetRestaurantInfo()
        {
            return restaurantInfo;
        }
    }
}
