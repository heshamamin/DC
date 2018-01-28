using System;
using System.Collections.Generic;
using System.Text;

namespace DesignCrowd
{
    public class BusinessDayCounter
    {
        public int WeekdaysBetweenTwoDates(DateTime firstDate, DateTime secondDate)
        {
            var adjustedFirstDate = AdjustFirstDate(firstDate);
            var adjustedSecondDate = AdjustSecondDate(secondDate);

            var nearestMonday = GetNearestMondayDate(adjustedFirstDate);

            if (nearestMonday > adjustedSecondDate)
            {
                return (int)adjustedSecondDate.Subtract(adjustedFirstDate).TotalDays;
            }

            var weekDays = adjustedFirstDate.Equals(nearestMonday) ? 0 : (int)nearestMonday.Subtract(adjustedFirstDate).TotalDays - 2;
            if (!(firstDate.DayOfWeek == DayOfWeek.Saturday || firstDate.DayOfWeek == DayOfWeek.Sunday))
            {
                weekDays--;
            }

            if (adjustedSecondDate > nearestMonday)
            {
                var total = (int)adjustedSecondDate.Subtract(nearestMonday).TotalDays;
                var weekendDays = 2 * (total / 7);
                weekDays += total - weekendDays;
            }

            return weekDays;
        }

        private static DateTime AdjustFirstDate(DateTime firstDate)
        {
            var adjustment = 0;
            switch (firstDate.DayOfWeek)
            {
                case DayOfWeek.Saturday:
                    adjustment = 2;
                    break;
                case DayOfWeek.Sunday:
                    adjustment = 1;
                    break;
                default:
                    adjustment = 0;
                    break;
            }
            return firstDate.AddDays(adjustment);
        }

        private static DateTime AdjustSecondDate(DateTime secondDate)
        {
            var adjustment = 0;
            switch (secondDate.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    adjustment = -1;
                    break;
                default:
                    adjustment = 0;
                    break;
            }
            return secondDate.AddDays(adjustment);
        }

        internal static DateTime GetNearestMondayDate(DateTime date)
        {
            var days = (8 - ((int)date.DayOfWeek)) % 7;
            return date.AddDays(days);
        }


        public int BusinessDaysBetweenTwoDates(DateTime firstDate, DateTime secondDate, IList<DateTime> publicHolidays)
        {
            return 0;
        }
    }
}
