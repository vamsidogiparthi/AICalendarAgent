using System.ComponentModel;
using Microsoft.SemanticKernel;

namespace AICalendarAgent.Brain.Plugins.FunctionPlugins
{
    public class TimePlugin
    {
        public TimePlugin() { }

        [KernelFunction("get_time")]
        [Description("Get the current time.")]
        public static string GetTime()
        {
            return DateTime.Now.ToString("HH:mm:ss");
        }

        [KernelFunction("get_date")]
        [Description("Get the current date.")]
        public static string GetDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd");
        }

        [KernelFunction("get_date_with_format")]
        [Description("Get the current date with requested fornmat")]
        public static string GetDate(string format)
        {
            return DateTime.Now.ToString(format);
        }

        [KernelFunction("get_time_and_date")]
        [Description("Get the current time and date.")]
        public static string GetTimeAndDate()
        {
            return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        [KernelFunction("get_time_and_date_with_format")]
        [Description("Get the current time and date with requested format.")]
        public static string GetTimeAndDate(string format)
        {
            return DateTime.Now.ToString(format);
        }

        [KernelFunction("get_local_time_zone")]
        [Description("Get the local time zone.")]
        public static string GetLocalTimeZone()
        {
            return TimeZoneInfo.Local.DisplayName;
        }

        [KernelFunction("get_local_time_zone_iana")]
        [Description("Get the local time zone in IANA format.")]
        public static string GetLocalTimeZoneIana()
        {
            return TimeZoneInfo.Local.Id;
        }
    }
}
