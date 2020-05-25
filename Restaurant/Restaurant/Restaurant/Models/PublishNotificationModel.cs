using System;
using System.Collections.Generic;
using System.Text;

namespace Restaurant.Models
{
    public class PublishNotificationModel
    {
        public string NotiChannel { get; set; }
        public object Params { get; set; }
    }
}
