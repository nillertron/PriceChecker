using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Services
{
    interface IPushNotificationRegister
    {
        void ExtractTokenAndRegister();
    }
}
