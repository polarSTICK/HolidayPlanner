using HolidayPlanner.Loggers;

namespace HolidayPlanner.Test
{
    public class FakeLogger : ILogger
    {
        public void Log(string message)
        {
            return;
        }
    }
}
