using DesignCrowd.Holidays;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignCrowd.Tests.Holidays
{
    public class HolidaysAggregatorTests
    {
        [Test]
        public void HolidaysAggregatorAggreagatesHolidaysFromPassedHolidayFactories()
        {
            var firstDate = new DateTime(2018, 1, 1);
            var secondDate = new DateTime(2018, 1, 5);
            var expected = new List<DateTime>()
            {
                new DateTime(2018, 1, 2),
                new DateTime(2018, 1, 3),
            };

            var holidayMock1 = new Mock<IHolidaysFactory>();
            var holidayMock2 = new Mock<IHolidaysFactory>();
            holidayMock1.Setup(h => h.CreateHolidays(It.Is<DateTime>(d => d.Equals(firstDate)), It.Is<DateTime>(d => d.Equals(secondDate)))).Returns(new List<DateTime>() { new DateTime(2018, 1, 2) });
            holidayMock2.Setup(h => h.CreateHolidays(It.Is<DateTime>(d => d.Equals(firstDate)), It.Is<DateTime>(d => d.Equals(secondDate)))).Returns(new List<DateTime>() { new DateTime(2018, 1, 3) });

            IHolidaysFactory sut = new HolidaysAggregator(new List<IHolidaysFactory>() {holidayMock1.Object, holidayMock2.Object });
            Assert.That(sut.CreateHolidays(firstDate, secondDate), Is.EquivalentTo(expected));
            holidayMock1.VerifyAll();
            holidayMock2.VerifyAll();
        }

    }
}
