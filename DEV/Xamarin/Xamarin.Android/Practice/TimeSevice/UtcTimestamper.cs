using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Practice.TimeSevice
{
    public class UtcTimestamper : IGetTimestamp
    {
        DateTime startTime;
        public UtcTimestamper()
        {
            startTime = DateTime.UtcNow;
        }
        public string GetFormattedTimestamp()
        {
            TimeSpan timeSpan = DateTime.UtcNow.Subtract(startTime);
            return $"Service started at {startTime} ({timeSpan:c} ago).";
        }
    }
}