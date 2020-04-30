using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Models
{
    public interface INotifications
    {
        void SaveSetting(int hours);
        void StopNotifications();
    }
}
