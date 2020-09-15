using System;

namespace LocalStore.Commons.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime GetCurrentDateTime()
        {
            return DateTime.Now;
        }
    }
}
