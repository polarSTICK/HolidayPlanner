using HolidayPlanner.Serializers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
        public void GetNationalHolidaysNotNullTest()
        {
            var dates = _planner.GetNationalHolidays();

            Assert.IsNotNull(dates);
        }
    }
}
