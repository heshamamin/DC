using NUnit.Framework;
using System;

namespace DesignCrowd.Tests
{
    public class BusinessDayCounterTests
    {
        [Test]
        public void WeekdaysBetweenTwoDates_RetrunsDaysWithinGivenRangeExcludingStartAndEnd()
        {
            var sut = new BusinessDayCounter();
            Assert.That(sut.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 7), new DateTime(2013, 10, 9)), Is.EqualTo(1));
        }

        [Test]
        public void WeekdaysBetweenTwoDates_RetrunsOnlyWeekDaysWithinGivenRangeExcludingStartAndEnd()
        {
            var sut = new BusinessDayCounter();
            Assert.That(sut.WeekdaysBetweenTwoDates(new DateTime(2013, 10, 5), new DateTime(2013, 10, 14)), Is.EqualTo(5));
        }

        [TestCase(2013, 10, 7, 2014, 1, 1, 61)]
        [TestCase(2013, 10, 7, 2015, 2, 10, 350)]
        public void WeekdaysBetweenTwoDatesInDifferentYears_RetrunsOnlyWeekDaysWithinGivenRangeExcludingStartAndEnd(int firstYear, int firstMonth, int firstDay, int secondYear, int secondMonth, int secondDay, int expected)
        {
            var sut = new BusinessDayCounter();
            var firstDate = new DateTime(firstYear, firstMonth, firstDay);
            var secondDate = new DateTime(secondYear, secondMonth, secondDay);
            Assert.That(sut.WeekdaysBetweenTwoDates(firstDate, secondDate), Is.EqualTo(expected));
        }

        [TestCase(2018, 1, 28, 2018, 1, 29)]
        [TestCase(2018, 1, 29, 2018, 1, 29)]
        [TestCase(2018, 1, 16, 2018, 1, 22)]
        public void GetNextMondayTest(int year, int month, int day, int expectedYear, int expectedMonth, int expectedDay)
        {
            var actual = BusinessDayCounter.GetNearestMondayDate(new DateTime(year, month, day));
            var expected = new DateTime(expectedYear, expectedMonth, expectedDay);

            Assert.That(actual, Is.EqualTo(expected));
        }



    }
}
