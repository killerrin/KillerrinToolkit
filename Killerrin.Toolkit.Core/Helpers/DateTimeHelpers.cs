using System;
using System.Collections.Generic;
using System.Text;

namespace Killerrin.Toolkit.Core.Helpers
{
    public static class DateTimeHelpers
    {
        public const int Minute = 60;
        public const int Hour = Minute * 60;
        public const int Day = Hour * 24;
        public const int Year = Day * 365;

        /// <summary>
        /// Returns the day which signifies the start of the week
        /// </summary>
        /// <param name="dt">A day in a week to get the start of the week from</param>
        /// <param name="startOfWeek">Your beginning day of the week</param>
        /// <returns>The date signifying the start of the week</returns>
        public static DateTime StartOfWeek(this DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = dt.DayOfWeek - startOfWeek;
            if (diff < 0)
            {
                diff += 7;
            }

            return dt.AddDays(-1 * diff).Date;
        }
        
        /// <summary>
        /// Converts Date to a relative time string compared against today
        /// </summary>
        /// <param name="utcValue">A UTC Date</param>
        /// <returns>A relative time string from today</returns>
        public static string ToRelativeDateTimeString(this DateTime utcValue)
        {
            return ToRelativeDateTimeString(DateTime.UtcNow, utcValue)
        }
        
        /// <summary>
        /// Converts Date to a relative time string
        /// </summary>
        /// <param name="fromDate">From Date</param>
        /// <param name="toDate">To Date</param>
        /// <returns>A relative time string</returns>
        public static string ToRelativeDateTimeString(DateTime fromDate, DateTime toDate)
        {
            // Calculate the Difference
            var difference = fromDate - toDate;

            // Exit early if inputs are invalid
            if (toDate == DateTime.MinValue) return "";
            else if (toDate == DateTime.MaxValue) return "";

            // Begin converting to relative time
            string result = "";
            if (difference.TotalSeconds < 2.0)
                result = "a second ago";
            else if (difference.TotalSeconds < Minute)
                result = Math.Floor(difference.TotalSeconds) + " seconds ago";

            else if (difference.TotalSeconds < Minute * 2)
                result = "a minute ago";
            else if (difference.TotalSeconds < Hour)
                result = Math.Floor(difference.TotalMinutes) + " minutes ago";

            else if (difference.TotalSeconds < Hour * 2)
                result = "an hour ago";
            else if (difference.TotalSeconds < Day)
                result = Math.Floor(difference.TotalHours) + " hours ago";

            else if (difference.TotalSeconds < Day * 2)
                result = "yesterday";
            else if (difference.TotalSeconds < Day * 30)
                result = Math.Floor(difference.TotalDays) + " days ago";

            else if (difference.TotalSeconds < Day * 60)
                result = "a month ago";
            else if (difference.TotalSeconds < Year)
                result = Math.Floor(difference.TotalDays / 30) + " months ago";

            // And because no one cares once its past a certain point, just display a year
            else result = toDate.ToString();

            //Debug.WriteLine(result.ToString());
            return result;
        }
    }
}
