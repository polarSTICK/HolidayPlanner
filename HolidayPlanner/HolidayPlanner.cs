using HolidayPlanner.Helpers;
using HolidayPlanner.Loggers;
using HolidayPlanner.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HolidayPlanner
{
    public class HolidayPlanner
    {
        private readonly IHolidaySerializer _holidaySerializer;
        private readonly ILogger _logger;

        public HolidayPlanner(IHolidaySerializer holidaySerializer, ILogger logger)
        {
            _holidaySerializer = holidaySerializer;
            _logger = logger;
        }

        public int CalculateAmountOfHolidays(DateTime startDate, DateTime endDate)
        {
            var errorState = PlannerHelper.ValidateDates(startDate, endDate);

            if (errorState != null)
                PlannerHelper.ShowError(errorState);

            var allUserDays = PlannerHelper.GetAllDatesInRange(startDate, endDate).ToList();
            _logger.Log($"In the time span you have chosen, the is/are {allUserDays.Count} days in total.");

            var nationalHolidays = GetNationalHolidays();

            var holidaysUsed = allUserDays.Where(x => x.DayOfWeek != DayOfWeek.Sunday && !nationalHolidays.Contains(x)).ToList();

            return holidaysUsed.Count;
        }

        public IEnumerable<DateTime> GetNationalHolidays()
        {
            return _holidaySerializer.GetHolidayDateTimes();
        }
    }
}
