using DesignCrowd.Holidays;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignCrowd.Tests.Holidays
{
    public class DayOfMonthHolidaysFactoryTests
    {
        [Test]
        public void CreateHolidaysTest()
        {
            IHolidaysFactory sut = new DayOfMonthHolidaysFactory(6, DayOfWeek.Monday, 2);
            var expected = new List<DateTime>()
            {
                new DateTime(2013, 6, 10),
                new DateTime(2014, 6, 9),
                new DateTime(2015, 6, 8),
            };

            Assert.That(sut.CreateHolidays(new DateTime(2012, 12, 28), new DateTime(2016, 3, 20)), Is.EquivalentTo(expected));
        }

    }
}
