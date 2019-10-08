using IdentityCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityCheck.Services
{
    public interface IDateTimeService
    {
        IEnumerable<TimeZoneInfo> GetAvailableTimeZones();
        Task SetUserTimeZone(ApplicationUser user, string timeZoneId);
        DateTime GetLocalDateTime(ApplicationUser user, DateTime utcDateTime);
    }
}
