using Android.App.Job;
using Android.Content;
using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Services
{
    public static class JobScheduleHelpers
    {
        public static JobInfo.Builder CreateJobBuilderUsingJobId<T>(Context context, int jobId) where T:JobService
        {
            var javaClass = Java.Lang.Class.FromType(typeof(T));
            var componentName = new ComponentName(context, javaClass);
            return new JobInfo.Builder(jobId, componentName);
        }
    }
}
