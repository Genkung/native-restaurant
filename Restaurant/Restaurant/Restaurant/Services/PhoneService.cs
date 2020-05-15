using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace Restaurant.Services
{
    public class PhoneService
    {
        public static void Call(string number)
        {
            try
            {
                PhoneDialer.Open(number);
            }
            catch (ArgumentNullException anEx)
            {
                App.Current.MainPage.DisplayAlert("แจ้งเตือน", "ไม่พบหมายเลขที่ต้องการโทร", "ปิด");
            }
            catch (FeatureNotSupportedException ex)
            {
                App.Current.MainPage.DisplayAlert("แจ้งเตือน", "ไม่สามารถใช้งานระบบโทรศัพย์ได้", "ปิด");
            }
            catch (Exception ex)
            {
                App.Current.MainPage.DisplayAlert("แจ้งเตือน", "ขออภัย เกิดข้อผิดพลาด", "ปิด");
            }
        }
    }
}
