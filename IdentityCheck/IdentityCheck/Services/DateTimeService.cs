using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityCheck.Models;

namespace IdentityCheck.Services
{
    public class DateTimeService : IDateTimeService
    {

        private readonly IUserService userService;

        public DateTimeService(IUserService userService)
        {
            this.userService = userService;
        }

        public IEnumerable<TimeZoneInfo> GetAvailableTimeZones()
        {
            return TimeZoneInfo.GetSystemTimeZones();
        }

        public DateTime GetLocalDateTime(ApplicationUser user, DateTime utcDateTime)
        {
            if (user?.TimeZoneId == null)
            {
                return utcDateTime;
            }

            var timezoneinfo = TimeZoneInfo.FindSystemTimeZoneById(user.TimeZoneId);

            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, timezoneinfo);

           // return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(utcDateTime, user.TimeZoneId);
        }

        public async Task SetUserTimeZone(ApplicationUser user, string timeZoneId)
        {
            if (!IsValidTimeZoneId(timeZoneId))
            {
                return;
            }

            user.TimeZoneId = timeZoneId;
            await userService.SaveUserAsync(user);
        }

        private bool IsValidTimeZoneId(string timeZoneId)
        {
            try
            {
                TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
                return true;
            }
            catch (TimeZoneNotFoundException)
            {
                return false;
            }
        }
    }
}
