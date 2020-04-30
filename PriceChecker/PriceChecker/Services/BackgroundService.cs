﻿using Android.App;
using Android.Content;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Services
{
    [Service]
    class BackgroundService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            return StartCommandResult.NotSticky;
        }
    }
}
