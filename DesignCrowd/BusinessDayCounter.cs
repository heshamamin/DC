using System;
using System.Collections.Generic;
using System.Linq;

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

        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            if (publicHolidays == null)
            {
                throw new ArgumentNullException(nameof(publicHolidays));
            }

            var businessDays = WeekdaysBetweenTwoDates(firstDate, secondDate);
            foreach (var holiday in publicHolidays.Distinct())
            {
                if (holiday >= firstDate && holiday < secondDate && holiday.DayOfWeek != DayOfWeek.Saturday && holiday.DayOfWeek != DayOfWeek.Sunday)
                {
                    businessDays--;
                }
            }
            return businessDays;
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
            var fullWeekends = CalculateFullWeekends(difference);
            int partialWeekends = CalculatePartialWeekends(difference);

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

        private static int CalculatePartialWeekends(int difference)
        {
            var partialWeekends = 0;
            if (difference % 7 == 5)
            {
                partialWeekends = 1;
            }
            else if (difference % 7 == 6)
            {
                partialWeekends = 2;
            }

            return partialWeekends;
        }

        private static int CalculateFullWeekends(int difference)
        {
            return 2 * (difference / 7);
        }


    }
}
