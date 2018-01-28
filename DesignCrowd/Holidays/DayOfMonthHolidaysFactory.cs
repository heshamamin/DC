using System;
using System.Collections.Generic;

namespace DesignCrowd.Holidays
{
    public class DayOfMonthHolidaysFactory : IHolidaysFactory
    {
        private int _month;
        private DayOfWeek _dayOfWeek;
        private int _week;

        public DayOfMonthHolidaysFactory(int month, DayOfWeek dayOfWeek, int week)
        {
            _month = month;
            _dayOfWeek = dayOfWeek;
            _week = week;
        }

        public IList<DateTime> CreateHolidays(DateTime start, DateTime end)
        {
            var holidays = new List<DateTime>();

            for (int year = start.Year; year <= end.Year; year++)
            {
                var day = CalculateDay(year, _month, _week);
                var holiday = new DateTime(year, _month, day);
                if (holiday >= start && holiday <= end)
                {
                    holidays.Add(holiday);
                }
            }

            return holidays;
        }

        private int CalculateDay(int year, int month, int week)
        {
            var startDate = new DateTime(year, month, 1).AddDays(-1);
            var currentWeek = 0;
            while (currentWeek < week)
            {
                startDate = startDate.AddDays(1);
                if (startDate.DayOfWeek == _dayOfWeek)
                {
                    currentWeek++;
                }
            }

            return startDate.Day;
        }
    }
}
