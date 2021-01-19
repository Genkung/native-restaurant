using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Restaurant.Droid;
using Restaurant.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(Audio))]
namespace Restaurant.Droid
{
    public class Audio : IAudio
    {
        private MediaPlayer _mediaPlayer;

        public bool PlayAudio()
        {
            try
            {
                _mediaPlayer = MediaPlayer.Create(global::Android.App.Application.Context, Resource.Raw.orderiscoming);
                _mediaPlayer.Start();
                return true;
            }
            catch (Exception e) { return false; }

        }
    }
}