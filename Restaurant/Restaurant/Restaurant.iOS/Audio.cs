using AVFoundation;
using Foundation;
using Restaurant.iOS;
using Restaurant.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(Audio))]
namespace Restaurant.iOS
{
    public class Audio : IAudio
    {
        AVAudioPlayer _mediaPlayer;
        public bool PlayAudio()
        {
            try
            {
                var fileName = "orderiscoming.mp3";
                string sFilePath = NSBundle.MainBundle.PathForResource
                  (Path.GetFileNameWithoutExtension(fileName), Path.GetExtension(fileName));
                var url = NSUrl.FromString(sFilePath);
                _mediaPlayer = AVAudioPlayer.FromUrl(url);
                _mediaPlayer.FinishedPlaying += (object sender, AVStatusEventArgs e) =>
                {
                    _mediaPlayer = null;
                };
                _mediaPlayer.Play();
                return true;
            }
            catch (Exception e) { return false; }


        }
    }
}