using Android.Content;
using Android.OS;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Services
{
    [BroadcastReceiver]
    class BackgroundReciever:BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            var pm = (PowerManager)context.GetSystemService(Context.PowerService);
            PowerManager.WakeLock wakeLock = pm.NewWakeLock(WakeLockFlags.Partial, "BackgroundReceiver");

            wakeLock.Release();
        }
    }
}
