using HolidayPlanner.Helpers;
using HolidayPlanner.Loggers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace HolidayPlanner.Test
{
    [TestClass]
    public class PlannerHelperTest : TestBase
    {
        private readonly ILogger _logger;

        public PlannerHelperTest()
        {
            _logger = new FakeLogger();
        }

        [TestMethod]
        public void HolidayPeriodDatesWrongOrderTest()
        {
            Enums.HolidayPlannerError? erroState;
            DateTime testStartDate = new DateTime(2021, 2, 1);
            DateTime testEndDate = new DateTime(2021, 1, 1);

            erroState = PlannerHelper.ValidateDates(testStartDate, testEndDate);

            Assert.AreEqual(erroState, Enums.HolidayPlannerError.EndDateEarlier);
        }

        [TestMethod]
        public void HolidayPeriodDatesExceedPeriodTest()
        {
            Enums.HolidayPlannerError? erroState;
            DateTime testStartDate = new DateTime(2021, 3, 3);
            DateTime testEndDate = new DateTime(2021, 4, 4);

            erroState = PlannerHelper.ValidateDates(testStartDate, testEndDate);

            Assert.AreEqual(erroState, Enums.HolidayPlannerError.ExceedHolidayPeriod);
        }

        [TestMethod]
        public void HolidayPeriodTooLongTest()
        {
            Enums.HolidayPlannerError? erroState;
            DateTime testStartDate = new DateTime(2021, 4, 1);
            DateTime testEndDate = new DateTime(2021, 6, 1);

            erroState = PlannerHelper.ValidateDates(testStartDate, testEndDate);

            Assert.AreEqual(erroState, Enums.HolidayPlannerError.PeriodTooLong);
        }

        [TestMethod]
        public void HolidayPeriodDatesAreValidTest()
        {
            Enums.HolidayPlannerError? erroState;
            DateTime testStartDate = new DateTime(2021, 6, 1);
            DateTime testEndDate = new DateTime(2021, 6, 30);

            erroState = PlannerHelper.ValidateDates(testStartDate, testEndDate);

            Assert.AreEqual(erroState, null);
        }

        [TestMethod]
        public void DateValidTest()
        {
            bool dateValid;

            dateValid = PlannerHelper.IsDateValid("1.7.2021", _logger);

            Assert.IsTrue(dateValid);
        }

        [TestMethod]
        public void DateNotValidTest()
        {
            bool dateValid;

            dateValid = PlannerHelper.IsDateValid("12.334.1234e", _logger);

            Assert.IsFalse(dateValid);
        }

        [TestMethod]
        public void GetAllDatesInRangeCorrectAmountTest()
        {
            DateTime testStartDate = new DateTime(2021, 4, 1);
            DateTime testEndDate = new DateTime(2021, 4, 30);

            var dates = PlannerHelper.GetAllDatesInRange(testStartDate, testEndDate).ToList();

            Assert.AreEqual(30, dates.Count);
        }

        [TestMethod]
        public void GetAllDatesInRangeDatesWrongTest()
        {
            DateTime testStartDate = new DateTime(2021, 4, 30);
            DateTime testEndDate = new DateTime(2021, 4, 1);

            var dates = PlannerHelper.GetAllDatesInRange(testStartDate, testEndDate).ToList();

            Assert.AreEqual(0, dates.Count);
        }

        [TestMethod]
        public void ShowErrorPeriodTooLongTest()
        {
            string errorMessage = string.Empty;

            try
            {
                PlannerHelper.ShowError(Enums.HolidayPlannerError.PeriodTooLong);
            }
            catch (ArgumentException e)
            {
                errorMessage = e.Message;
            }
            
            Assert.AreEqual(errorMessage, "Time span must be less than 50 days.");
        }

        [TestMethod]
        public void ShowErrorNullParameterPassedTest()
        {
            string errorMessage = "This is not an empty string";

            try
            {
                PlannerHelper.ShowError(null);
            }
            catch (ArgumentException e)
            {
                errorMessage = e.Message;
            }

            Assert.AreEqual(errorMessage, string.Empty);
        }
    }
}
