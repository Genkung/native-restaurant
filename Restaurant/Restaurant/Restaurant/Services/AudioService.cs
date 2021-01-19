using Restaurant.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Restaurant.Services
{
    public class AudioService
    {
        public static void PlayNotificationSound() 
        {
            DependencyService.Get<IAudio>().PlayAudio();
        }
    }
}
