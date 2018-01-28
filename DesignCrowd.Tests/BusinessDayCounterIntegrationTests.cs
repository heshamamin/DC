using DesignCrowd.Holidays;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignCrowd.Tests
{
    public class BusinessDayCounterIntegrationTests
    {
        [Test]
        public void BusinessDayCounterIntegrationWithAnnualHolidaysTests()
        {
            var newYear = new FirstWeekdayHolidayShiftingDecorator(new FixedDateHolidaysFactory(1, 1));
            var christmas = new FixedDateHolidaysFactory(12, 25);
            var boxing = new FixedDateHolidaysFactory(12, 26);
            var queenDay = new DayOfMonthHolidaysFactory(6, DayOfWeek.Monday, 2);

            var aggregator = new HolidaysAggregator(new List<IHolidaysFactory>()
            {
                newYear,
                christmas,
                boxing,
                queenDay
            });

            var counter = new BusinessDayCounter();

            Assert.That(counter.BusinessDaysBetweenTwoDates(new DateTime(2016, 12, 20), new DateTime(2018, 2, 1), aggregator), Is.EqualTo(291 - 6));

        }

    }
}
