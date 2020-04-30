using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;

using Android.OS;
using Android.Content;
using Android.App.Job;
using PriceChecker.Services;
using PriceChecker.Droid.Service;
using Android.Support.V4.App;
using Android.Media;
using Android.Views;
using Android.Widget;

namespace PriceChecker.Droid
{
    [Activity(Label = "PriceChecker", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly int NOTIFICATION_ID = 1000;
        static readonly string CHANNEL_ID = "location_notification";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            bool flag = false;
            if (Intent.Extras != null)
            {
                flag = true;
            }
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App(flag));
            CreateNotificationsChannel();
            Notifications.ma = this;

        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
        #region Notifications



        public void CreateNotificationsChannel()
        {
            //tjek version
            if (Build.VERSION.SdkInt < BuildVersionCodes.O)
            {
                return;
            }
            //Få channelname fra selvlavet XML fil kaldet String.xml
            var channelName = Resources.GetString(Resource.String.channel_name);
            var channelDescription = Resources.GetString(Resource.String.channel_description);
            //lav channel
            var channel = new NotificationChannel(CHANNEL_ID, channelName, NotificationImportance.Default)
            {
                Description = channelDescription
            };
            //få notifikation services
            var notificationManager = (NotificationManager)GetSystemService(NotificationService);
            //opret channel
            notificationManager.CreateNotificationChannel(channel);
        }
      
        #endregion


    }
}