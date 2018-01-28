using System;
using System.Collections.Generic;

namespace DesignCrowd.Holidays
{
    public class FirstWeekdayHolidayShiftingDecorator : IHolidaysFactory
    {
        private IHolidaysFactory _originalFactory;

        public FirstWeekdayHolidayShiftingDecorator(IHolidaysFactory originalFactory)
        {
            _originalFactory = originalFactory;
        }

        public IList<DateTime> CreateHolidays(DateTime start, DateTime end)
        {
            var holidays = new List<DateTime>();
            var originalHolidays = _originalFactory.CreateHolidays(start, end);

            foreach (var holiday in originalHolidays)
            {
                var adjusted = holiday;
                if (adjusted.DayOfWeek == DayOfWeek.Saturday)
                {
                    adjusted = adjusted.AddDays(2);
                }
                else if (adjusted.DayOfWeek == DayOfWeek.Sunday)
                {
                    adjusted = adjusted.AddDays(1);
                }
                if (adjusted <= end)
                {
                    holidays.Add(adjusted);
                }
            }

            return holidays;
        }
    }
}
