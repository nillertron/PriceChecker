using System;
using Xamarin.Forms;
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

[assembly: Dependency(typeof(PriceChecker.Droid.Service.Notifications))]
namespace PriceChecker.Droid.Service
{
    public class Notifications : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, Models.INotifications
    {
        public static MainActivity ma { get; set; }
        static readonly string CHANNEL_ID = "location_notification";
        public void SaveSetting(int hours)
        {
            //Start med at lave en job builder, og fortæl den at servicen skal være af typen download service
            var jobBuilder = JobScheduleHelpers.CreateJobBuilderUsingJobId<DownloadService>(ma,1);
            //sæt properties på jobbet, skal det være wifi, tidsinterval osv
            var jobInfo = jobBuilder.SetRequiresDeviceIdle(true).SetPersisted(true).SetRequiredNetworkType(NetworkType.NotRoaming).SetPeriodic((long)TimeSpan.FromHours(hours).TotalMilliseconds).Build();

            var jobScheduler = (JobScheduler)ma.GetSystemService(JobSchedulerService);
            jobScheduler.Schedule(jobInfo);

            //giv denne instans til downloadservicen, så den kan kalde notifikations metoden 
            Service.DownloadService.instance = ma;
        }



        public void StopNotifications()
        {
            var jobScheduler = (JobScheduler)ma.GetSystemService(JobSchedulerService);
            jobScheduler.Cancel(1);
        }
        public void SendNotification(string titel, string content)
        {
            //lav intent for at kunne agere på at notifikationen bliver tabbed
            var channelName = ma.Resources.GetString(Resource.String.channel_name);
            var channelDescription = ma.Resources.GetString(Resource.String.channel_description);
            var context = (global::Android.App.Application.Context);
            var intent = new Intent(context, typeof(MainActivity));
            intent.PutExtra(channelName, channelDescription);
            intent.AddFlags(ActivityFlags.ClearTop);
            //lav pendingintent
            var pendingIntent = PendingIntent.GetActivity(context, 0, intent, PendingIntentFlags.OneShot);


            // Instantiate the builder and set notification elements:
            //content og titel er hvad der kommer til at stå i beskeden
            //husk at sætte contentintent for at få en reaktionsmetode
            NotificationCompat.Builder builder = new NotificationCompat.Builder(ma, CHANNEL_ID)
                .SetContentTitle(titel)
                .SetContentText(content)
                .SetSound(RingtoneManager.GetDefaultUri(RingtoneType.Notification))
                //.SetSmallIcon(Resource.Drawable.ic_mtrl_chip_checked_circle)
                .SetSmallIcon(Resource.Drawable.money)
                .SetContentIntent(pendingIntent);

            // Build the notification:
            Notification notification = builder.Build();
            //Sæt cancel på klik
            notification.Flags = NotificationFlags.AutoCancel;
            // Get the notification manager:
            NotificationManager notificationManager =
                ma.GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }
    }
}