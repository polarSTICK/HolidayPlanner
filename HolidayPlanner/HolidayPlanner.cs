using System;
using System.Collections.Generic;
using System.Linq;
using HolidayPlanner.Loggers;
using HolidayPlanner.Serializers;

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
            CheckIfInputValid(startDate, endDate);

            var allUserDays = GetAllDatesInRange(startDate, endDate).ToList();

            var nationalHolidays = GetNationalHolidays();

            var holidaysUsed = allUserDays.Where(x => x.DayOfWeek != DayOfWeek.Sunday && !nationalHolidays.Contains(x)).ToList();

            return holidaysUsed.Count;
        }

        public void CheckIfInputValid(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
                throw new ArgumentException("End date must be greater than or equal to start date.");

            if (CheckIfPeriodTooLong(startDate, endDate))
                throw new ArgumentException("Time span must be less than 50 days.");

            if (CheckIfSameHolidayPeriod(startDate, endDate))
                throw new ArgumentException("Time span must be within the same holiday period.");
        }

        public bool CheckIfSameHolidayPeriod(DateTime startDate, DateTime endDate)
        {
            var startDateOk = startDate <= new DateTime(startDate.Year, 3, 31);
            var endDateOk = new DateTime(endDate.Year, 4, 1) <= endDate;

            return startDateOk && endDateOk;
        }

        public bool CheckIfPeriodTooLong(DateTime startDate, DateTime endDate)
        {
            return (endDate - startDate).Days > 50;
        }

        public bool IsDateValid(string dateString)
        {
            try
            {
                _ = DateTime.Parse(dateString);
                return true;
            }
            catch (Exception)
            {
                _logger.Log("Date not valid, please write it again using format d.M.yyyy");
                return false;
            }
        }

        public IEnumerable<DateTime> GetNationalHolidays()
        { 
            return _holidaySerializer.GetHolidayDateTimes();
        }

        public IEnumerable<DateTime> GetAllDatesInRange(DateTime startDate, DateTime endDate)
        {
            var dates = new List<DateTime>();

            for (var dt = startDate; dt <= endDate; dt = dt.AddDays(1))
            {
                dates.Add(dt);
            }

            return dates;
        }
    }
}
