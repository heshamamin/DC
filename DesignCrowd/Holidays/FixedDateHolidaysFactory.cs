using System;
using System.Collections.Generic;

namespace DesignCrowd.Holidays
{
    public class FixedDateHolidaysFactory : IHolidaysFactory
    {
        private int _month;
        private int _day;

        public FixedDateHolidaysFactory(int month, int day)
        {
            _month = month;
            _day = day;
        }

        public IList<DateTime> CreateHolidays(DateTime start, DateTime end)
        {
            var holidays = new List<DateTime>();
            for (int year = start.Year; year <= end.Year; year++)
            {
                var holiday = new DateTime(year, _month, _day);
                if (holiday >= start && holiday <= end)
                {
                    holidays.Add(holiday);
                }
            }

            return holidays;
        }
    }
}
