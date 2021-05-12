using HolidayPlanner.Loggers;
using System;
using System.Collections.Generic;

namespace HolidayPlanner.Helpers
{
    public static class PlannerHelper
    {
        public static IEnumerable<DateTime> GetAllDatesInRange(DateTime startDate, DateTime endDate)
        {
            var dates = new List<DateTime>();

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates;
        }

        public static Enums.HolidayPlannerError? ValidateDates(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                return Enums.HolidayPlannerError.EndDateEarlier;

            if (CheckIfPeriodTooLong(startDate, endDate))
                return Enums.HolidayPlannerError.PeriodTooLong;

            if (CheckIfSameHolidayPeriod(startDate, endDate))
                return Enums.HolidayPlannerError.ExceedHolidayPeriod;

            return null;
        }

        //TODO: Bring ILogger dependency some better way
        public static bool IsDateValid(string dateString, ILogger logger)
        {
            try
            {
                _ = DateTime.Parse(dateString);
                return true;
            }
            catch (Exception)
            {
                logger.Log("Date not valid, please write it again using format d.M.yyyy");
                return false;
            }
        }

        public static void ShowError(Enums.HolidayPlannerError? errorState)
        {
            var errorMessage = string.Empty;

            switch (errorState)
            {
                case Enums.HolidayPlannerError.EndDateEarlier:
                    errorMessage = "End date must be greater than or equal to start date.";
                    break;
                case Enums.HolidayPlannerError.PeriodTooLong:
                    errorMessage = "Time span must be less than 50 days.";
                    break;
                case Enums.HolidayPlannerError.ExceedHolidayPeriod:
                    errorMessage = "Time span must be within the same holiday period.";
                    break;
            }

            throw new ArgumentException(errorMessage);
        }

        private static bool CheckIfSameHolidayPeriod(DateTime startDate, DateTime endDate)
        {
            var startDateOk = startDate <= new DateTime(startDate.Year, 3, 31);
            var endDateOk = new DateTime(endDate.Year, 4, 1) <= endDate;

            return startDateOk && endDateOk;
        }

        private static bool CheckIfPeriodTooLong(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days > 50;
        }
    }
}
