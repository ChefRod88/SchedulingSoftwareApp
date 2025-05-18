using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchedulingSoftwareApp
{
    public class TimeHelper
    {
        private static readonly TimeZoneInfo estZone = TimeZoneInfo.FindSystemTimeZoneById("Eastern Standard Time");

        public static DateTime ToEST(DateTime utcTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcTime, estZone);
        }

        public static DateTime ToUTCFromEST(DateTime estTime)
        {
            return TimeZoneInfo.ConvertTimeToUtc(estTime, estZone);
        }
    }
}
