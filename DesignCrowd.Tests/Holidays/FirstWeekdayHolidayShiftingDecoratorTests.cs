using DesignCrowd.Holidays;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace DesignCrowd.Tests.Holidays
{
    public class FirstWeekdayHolidayShiftingDecoratorTests
    {
        [Test]
        public void CreateHolidaysTest()
        {
            var originalFactoryMock = new Mock<IHolidaysFactory>();
            var startDate = new DateTime(2012, 12, 28);
            var endDate = new DateTime(2018, 1, 27);
            originalFactoryMock.Setup(f => f.CreateHolidays(It.Is<DateTime>(d => d.Equals(startDate)), It.Is<DateTime>(d => d.Equals(endDate))))
                .Returns(new List<DateTime>()
                {
                    new DateTime(2017, 1, 1),
                    new DateTime(2018, 1, 1),
                    new DateTime(2018, 1, 6),
                    new DateTime(2018, 1, 27),
                });

            IHolidaysFactory sut = new FirstWeekdayHolidayShiftingDecorator(originalFactoryMock.Object);

            var expected = new List<DateTime>()
            {
                new DateTime(2017, 1, 2),
                new DateTime(2018, 1, 1),
                new DateTime(2018, 1, 8),
            };

            Assert.That(sut.CreateHolidays(startDate, endDate), Is.EquivalentTo(expected));
            originalFactoryMock.VerifyAll();
        }
    }
}
