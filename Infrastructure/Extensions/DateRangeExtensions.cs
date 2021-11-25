using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Infrastructure.Extensions
{
    public static class DateRangeExtensions
    {
        public static IEnumerable<DateTime> GetDateRange(DateTime startDate, DateTime endDate)
        {
            while (startDate.Date <= endDate.Date)
            {
                yield return startDate;
                startDate = startDate.AddDays(1);
            }
        }

    }
}
