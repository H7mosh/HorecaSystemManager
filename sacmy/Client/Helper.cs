﻿namespace sacmy.Client
{
    public class Helper
    {
        public static string GetRelativeTime(DateTime dateTime)
        {
            var timeSpan = DateTime.Now - dateTime;

            if (timeSpan <= TimeSpan.FromSeconds(60))
                return $"{timeSpan.Seconds} seconds ago";
            else if (timeSpan <= TimeSpan.FromMinutes(60))
                return timeSpan.Minutes > 1 ? $"{timeSpan.Minutes} minutes ago" : "a minute ago";
            else if (timeSpan <= TimeSpan.FromHours(24))
                return timeSpan.Hours > 1 ? $"{timeSpan.Hours} hours ago" : "an hour ago";
            else if (timeSpan <= TimeSpan.FromDays(7))
                return timeSpan.Days > 1 ? $"{timeSpan.Days} days ago" : "yesterday";
            else if (timeSpan <= TimeSpan.FromDays(30))
                return timeSpan.Days > 7 ? $"{timeSpan.Days / 7} weeks ago" : "a week ago";
            else if (timeSpan <= TimeSpan.FromDays(365))
                return timeSpan.Days > 30 ? $"{timeSpan.Days / 30} months ago" : "a month ago";
            else
                return timeSpan.Days > 365 ? $"{timeSpan.Days / 365} years ago" : "a year ago";
        }

    }
}
