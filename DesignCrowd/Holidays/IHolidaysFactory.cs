using System;
using System.Collections.Generic;

namespace DesignCrowd.Holidays
{
    public interface IHolidaysFactory
    {
        IList<DateTime> CreateHolidays(DateTime start, DateTime end);
    }
}
