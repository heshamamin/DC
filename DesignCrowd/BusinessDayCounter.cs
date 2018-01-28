using System;
using System.Collections.Generic;

namespace DesignCrowd
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            if (secondDate.Date <= firstDate.Date)
            {
                return 0;
            }

            var baseMonday = GetBaseMondayDate(firstDate);
            var workdaysToFirstDate = CalculateWorkDaysFromBaseMonday(baseMonday, firstDate);
            var workdaysToSecondDate = CalculateWorkDaysFromBaseMonday(baseMonday, secondDate);

            var secondDateAdjustment = CalculateDateAdjustment(secondDate);

            return workdaysToSecondDate - workdaysToFirstDate + secondDateAdjustment;
        }

        private int CalculateDateAdjustment(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday)
            {
                return 0;
            }
            return -1;
        }

        internal static int CalculateWorkDaysFromBaseMonday(DateTime baseMonday, DateTime date)
        {
            var difference = (int)Math.Round(date.Subtract(baseMonday).TotalDays, 0);
            var fullWeekends = 2 * (difference / 7);
            var partialWeekends = 0;
            if (difference % 7 == 5)
            {
                partialWeekends = 1;
            }
            else if (difference % 7 == 6)
            {
                partialWeekends = 2;
            }

            difference = difference - fullWeekends - partialWeekends;
            return difference;
        }

        internal static DateTime GetBaseMondayDate(DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Monday)
            {
                return date;
            }
            var days = (8 - ((int)date.DayOfWeek)) % 7;
            return date.AddDays(days - 7);
        }

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            return 0;
        }
    }
}
