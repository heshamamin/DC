using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace DesignCrowd.Tests
{
    public class BusinessDayCounterTests
    {
        private BusinessDayCounter Sut { get; set; }

        [SetUp]
        public void Setup()
        {
            Sut = new BusinessDayCounter();
        }


        [Test]
        public void WeekdaysBetweenTwoDates_RetrunsDaysWithinGivenRangeExcludingStartAndEnd()
        {
            Assert.That(Sut.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)), Is.EqualTo(1));
        }

        [Test]
        public void WeekdaysBetweenTwoDates_RetrunsOnlyWeekDaysWithinGivenRangeExcludingStartAndEnd()
        {
            Assert.That(Sut.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)), Is.EqualTo(5));
        }

        [TestCase(2013, 10, 7, 2014, 1, 1, 61)]
        [TestCase(2013, 10, 7, 2015, 2, 10, 350)]
        public void WeekdaysBetweenTwoDatesInDifferentYears_RetrunsOnlyWeekDaysWithinGivenRangeExcludingStartAndEnd(int firstYear, int firstMonth, int firstDay, int secondYear, int secondMonth, int secondDay, int expected)
        {
            var firstDate = new DateTime(firstYear, firstMonth, firstDay);
            var secondDate = new DateTime(secondYear, secondMonth, secondDay);
            Assert.That(Sut.WeekdaysBetweenTwoDates(firstDate, secondDate), Is.EqualTo(expected));
        }

        [TestCase(2013, 10, 7, 2013, 10, 7)]
        [TestCase(2013, 10, 7, 2013, 10, 5)]
        public void WeekdaysBetweenTwoDatesWithZeroIntervals_ReturnsZero(int firstYear, int firstMonth, int firstDay, int secondYear, int secondMonth, int secondDay)
        {
            var firstDate = new DateTime(firstYear, firstMonth, firstDay);
            var secondDate = new DateTime(secondYear, secondMonth, secondDay);
            Assert.That(Sut.WeekdaysBetweenTwoDates(firstDate, secondDate), Is.EqualTo(0));
        }

        [TestCase(2018, 1, 28, 2018, 1, 22)]
        [TestCase(2018, 1, 29, 2018, 1, 29)]
        [TestCase(2018, 1, 16, 2018, 1, 15)]
        public void GetBaseMondayDateTest(int year, int month, int day, int expectedYear, int expectedMonth, int expectedDay)
        {
            var actual = BusinessDayCounter.GetBaseMondayDate(new DateTime(year, month, day));
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay);

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(2018, 1, 22, 2018, 1, 22, 0)]
        [TestCase(2018, 1, 29, 2018, 1, 30, 1)]
        [TestCase(2018, 1, 15, 2018, 1, 23, 6)]
        [TestCase(2018, 1, 15, 2018, 1, 22, 5)]
        [TestCase(2018, 1, 15, 2018, 1, 21, 4)]
        [TestCase(2018, 1, 15, 2018, 1, 20, 4)]
        public void CalculateWorkDaysFromBaseMondayTest(int baseYear, int baseMonth, int baseDay, int year, int month, int day, int expected)
        {
            var actual = BusinessDayCounter.CalculateWorkDaysFromBaseMonday(new DateTime(baseYear, baseMonth, baseDay), new DateTime(year, month, day));
            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase(2013, 10, 7, 2013, 10, 9, 1)]
        [TestCase(2013, 12, 24, 2013, 12, 27, 0)]
        [TestCase(2013, 10, 7, 2014, 1, 1, 59)]
        public void BusinessDaysBetweenTwoDatesTest(int firstYear, int firstMonth, int firstDay, int secondYear, int secondMonth, int secondDay, int expected)
        {
            var vacations = new List<DateTime>()
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1),
            };
            var actual = Sut.BusinessDaysBetweenTwoDates(new DateTime(firstYear, firstMonth, firstDay), new DateTime(secondYear, secondMonth, secondDay), vacations);
            Assert.That(actual, Is.EqualTo(expected));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_NullPublicHolidaysThrowsArgumentNullException()
        {
            var exception = Assert.Throws<ArgumentNullException>(() => Sut.BusinessDaysBetweenTwoDates(new DateTime(2013, 12, 25), new DateTime(2013, 12, 26), null));
            Assert.That(exception.ParamName, Is.EqualTo("publicHolidays"));
        }

        [TestCase(2013, 12, 26, 2013, 12, 26)]
        [TestCase(2013, 12, 26, 2013, 12, 25)]
        public void BusinessDaysBetweenTwoDatesWithZeroIntervals_ReturnsZero(int firstYear, int firstMonth, int firstDay, int secondYear, int secondMonth, int secondDay)
        {
            var firstDate = new DateTime(firstYear, firstMonth, firstDay);
            var secondDate = new DateTime(secondYear, secondMonth, secondDay);
            var vacations = new List<DateTime>()
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1),
            };
            Assert.That(Sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, vacations), Is.EqualTo(0));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_DuplicateHolidaysCalculatedOnce()
        {
            var firstDate = new DateTime(2013, 10, 7);
            var secondDate = new DateTime(2014, 1, 1);
            var vacations = new List<DateTime>()
            {
                new DateTime(2013, 12, 25),
                new DateTime(2013, 12, 26),
                new DateTime(2013, 12, 26),
                new DateTime(2014, 1, 1),
            };
            Assert.That(Sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, vacations), Is.EqualTo(59));
        }

        [Test]
        public void BusinessDaysBetweenTwoDates_HolidayOnWeekendIgnored()
        {
            var firstDate = new DateTime(2017, 12, 28);
            var secondDate = new DateTime(2018, 1, 2);
            var vacations = new List<DateTime>()
            {
                new DateTime(2017, 12, 30),
                new DateTime(2017, 12, 31),
            };
            Assert.That(Sut.BusinessDaysBetweenTwoDates(firstDate, secondDate, vacations), Is.EqualTo(2));
        }

    }
}
