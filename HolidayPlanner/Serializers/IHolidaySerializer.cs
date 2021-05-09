using System;
using System.Collections.Generic;

namespace HolidayPlanner.Serializers
{
    public interface IHolidaySerializer
    {
        List<DateTime> GetHolidayDateTimes();
    }
}
