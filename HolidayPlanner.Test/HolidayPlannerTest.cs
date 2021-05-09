using System;
using System.Linq;
using HolidayPlanner.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HolidayPlanner.Test
{
    [TestClass]
    public class HolidayPlannerTest : TestBase
    {
        private readonly HolidayPlanner _planner;

        public HolidayPlannerTest()
        {
            _planner = new HolidayPlanner(new JsonHolidaySerializer(), new FakeLogger());
        }

        [TestMethod]
        public void CalculateOneWeekOfHoliday()
        {
            double result;
            DateTime testStartDate = new DateTime(2021, 5, 17);
            DateTime testEndDate = new DateTime(2021, 5, 23);

            result = _planner.CalculateAmountOfHolidays(testStartDate, testEndDate);

            Assert.AreEqual(6, result);
        }

        [TestMethod]
        public void CalculateChristmanHolidays()
        {
            double result;
            DateTime testStartDate = new DateTime(2021, 12, 20);
            DateTime testEndDate = new DateTime(2022, 1, 9);

            result = _planner.CalculateAmountOfHolidays(testStartDate, testEndDate);

            Assert.AreEqual(15, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void CalculateDaysInWrongOrder()
        {
            DateTime testStartDate = new DateTime(2021, 5, 1);
            DateTime testEndDate = new DateTime(2021, 4, 15);

            _planner.CalculateAmountOfHolidays(testStartDate, testEndDate);
        }

        [TestMethod]
        public void CheckIfSameHolidayPeriodIsNotTest()
        {
            bool dateValid;
            DateTime testStartDate = new DateTime(2021, 3, 1);
            DateTime testEndDate = new DateTime(2021, 4, 1);

            dateValid = _planner.CheckIfSameHolidayPeriod(testStartDate, testEndDate);

            Assert.IsTrue(dateValid);
        }

        [TestMethod]
        public void CheckIfSameHolidayPeriodIsSameTest()
        {
            bool dateValid;
            DateTime testStartDate = new DateTime(2021, 12, 1);
            DateTime testEndDate = new DateTime(2022, 1, 2);

            dateValid = _planner.CheckIfSameHolidayPeriod(testStartDate, testEndDate);

            Assert.IsFalse(dateValid);
        }

        [TestMethod]
        public void CheckIfPeriodTooLongIsCorrectLenghtTest()
        {
            bool dateValid;
            DateTime testStartDate = new DateTime(2021, 4, 1);
            DateTime testEndDate = new DateTime(2021, 4, 30);

            dateValid = _planner.CheckIfPeriodTooLong(testStartDate, testEndDate);

            Assert.IsFalse(dateValid);
        }

        [TestMethod]
        public void CheckIfPeriodTooLongIsLongTest()
        {
            bool dateValid;
            DateTime testStartDate = new DateTime(2021, 4, 1);
            DateTime testEndDate = new DateTime(2021, 6, 30);

            dateValid = _planner.CheckIfPeriodTooLong(testStartDate, testEndDate);

            Assert.IsTrue(dateValid);
        }

        [TestMethod]
        public void DateValidTest()
        {
            bool dateValid;

            dateValid = _planner.IsDateValid("1.7.2021");

            Assert.IsTrue(dateValid);
        }

        [TestMethod]
        public void DateNotValidTest()
        {
            bool dateValid;

            dateValid = _planner.IsDateValid("12.334.1234e");

            Assert.IsFalse(dateValid);
        }

        [TestMethod]
        public void GetNationalHolidaysNotNullTest()
        {
            var dates = _planner.GetNationalHolidays();

            Assert.IsNotNull(dates);
        }

        [TestMethod]
        public void GetAllDatesInRangeCorrectAmountTest()
        {
            DateTime testStartDate = new DateTime(2021, 4, 1);
            DateTime testEndDate = new DateTime(2021, 4, 30);

            var dates = _planner.GetAllDatesInRange(testStartDate, testEndDate).ToList();

            Assert.AreEqual(30, dates.Count);
        }

        [TestMethod]
        public void GetAllDatesInRangeDatesWrongTest()
        {
            DateTime testStartDate = new DateTime(2021, 4, 30);
            DateTime testEndDate = new DateTime(2021, 4, 1);

            var dates = _planner.GetAllDatesInRange(testStartDate, testEndDate).ToList();

            Assert.AreEqual(0, dates.Count);
        }
    }
}
