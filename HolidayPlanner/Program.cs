using HolidayPlanner.Helpers;
using HolidayPlanner.Loggers;
using HolidayPlanner.Serializers;
using System;

namespace HolidayPlanner
{
    class Program
    {
        private static ILogger _logger;

        //TODO: make a retry available
        static void Main(string[] args)
        {
            _logger = new ConsoleLogger();

            _logger.Log("Initializing HolidayPlanner");
            var planner = new HolidayPlanner(new JsonHolidaySerializer(), _logger);

            _logger.Log("Enter planned start date:");
            var startDateString = Console.ReadLine();

            while (!PlannerHelper.IsDateValid(startDateString, _logger))
                startDateString = Console.ReadLine();

            var startDateTime = DateTime.Parse(startDateString);

            _logger.Log("Enter planned end date:");
            var endDateString = Console.ReadLine();

            while (!PlannerHelper.IsDateValid(endDateString, _logger))
                endDateString = Console.ReadLine();

            var endDateTime = DateTime.Parse(endDateString);

            try
            {
                int holidaysNeeded = planner.CalculateAmountOfHolidays(startDateTime, endDateTime);

                _logger.Log($"For specified time period you need to use {holidaysNeeded} holidays");
            }
            catch (Exception e)
            {
                _logger.Log(e.Message);
            }
        }
    }
}