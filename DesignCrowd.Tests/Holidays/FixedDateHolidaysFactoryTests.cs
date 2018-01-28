using DesignCrowd.Holidays;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignCrowd.Tests.Holidays
{
    public class FixedDateHolidaysFactoryTests
    {

        [Test]
        public void CreateHolidaysTest()
        {
            IHolidaysFactory sut = new FixedDateHolidaysFactory(12, 26);
            var expected = new List<DateTime>()
            {
                new DateTime(2013, 12, 26),
                new DateTime(2014, 12, 26),
                new DateTime(2015, 12, 26),
            };

            Assert.That(sut.CreateHolidays(new DateTime(2012, 12, 28), new DateTime(2016, 12, 20)), Is.EquivalentTo(expected));
        }

    }
}
