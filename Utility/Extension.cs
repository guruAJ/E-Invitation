using System;

namespace E_Invitation.Utility
{
    internal static class ExtensionMethods
    {
        internal static string ToCastString(this object value)
        {
            return value != null ? value.ToString() : null;
        }
        internal static int ToInt32(this object value)
        {
            return Convert.ToInt32(value ?? 0);
        }
        internal static long ToInt64(this object value)
        {
            return Convert.ToInt64(value ?? 0);
        }
        internal static short ToInt16(this object value)
        {
            return Convert.ToInt16(value ?? 0);
        }
        internal static bool IsNotNull(this object value)
        {
            return value != null;
        }
        internal static bool IsGreaterThanZero(this int value)
        {
            return value > 0;
        }
        internal static bool IsGreaterThanZero(this long value)
        {
            return value > 0;
        }
        internal static bool IsGreaterThanZero(this short value)
        {
            return value > 0;
        }
        internal static DateTime ToDateTime(this string value)
        {
            return Convert.ToDateTime(value);
        }
        internal static DateTime ToDateTimeFromUnix(this long unixTimeStamp)
        {
            DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        internal static long ToUnixTime(this DateTime datetime)
        {
            return ((DateTimeOffset)datetime).ToUnixTimeSeconds();
        }

    }
    internal static class Get
    {
        internal static DateTime DateTimeNow()
        {
            return DateTime.Now;
        }
        internal static long UnixTimeNow()
        {
           return DateTimeOffset.Now.ToUnixTimeSeconds();
        }

    }
}
