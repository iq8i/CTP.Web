using System;
using System.Globalization;

namespace CTP.Application.Helpers
{
    public static class DateHelper
    {
        /// <summary>
        /// يحول التاريخ الميلادي إلى نص هجري منسق
        /// </summary>
        public static string ToHijri(this DateTime date)
        {
            var culture = new CultureInfo("ar-SA");
            // تثبيت تقويم أم القرى
            culture.DateTimeFormat.Calendar = new UmAlQuraCalendar();

            return date.ToString("dd/MM/yyyy", culture);
        }

        /// <summary>
        /// يحول التاريخ الميلادي (القابل للخواء) إلى نص هجري منسق
        /// </summary>
        public static string ToHijri(this DateTime? date)
        {
            return date.HasValue ? date.Value.ToHijri() : "-";
        }
    }
}