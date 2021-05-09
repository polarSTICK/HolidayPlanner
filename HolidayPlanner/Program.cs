using System;
using HolidayPlanner.Loggers;
using HolidayPlanner.Serializers;

namespace HolidayPlanner
{
    class Program
    {
        private static ILogger _logger;

        static void Main(string[] args)
        {
            _logger = new ConsoleLogger();

            _logger.Log("Initializing HolidayPlanner");

            var planner = new HolidayPlanner(new JsonHolidaySerializer(), _logger);

            _logger.Log("Enter planned start date:");

            var startDateString = Console.ReadLine();

            while (!planner.IsDateValid(startDateString))
            {
                startDateString = Console.ReadLine();
            }

            var startDateTime = DateTime.Parse(startDateString);

            _logger.Log("Enter planned end date:");

            var endDateString = Console.ReadLine();

            while (!planner.IsDateValid(endDateString))
            {
                endDateString = Console.ReadLine();
            }

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
//TODO: make a retry available