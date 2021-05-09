using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace HolidayPlanner.Serializers
{
    public class JsonHolidaySerializer : IHolidaySerializer
    {
        public List<DateTime> GetHolidayDateTimes()
        {
            //TODO: RelativePath
            string jsonFilePath = @"E:\Projects\HolidayPlanner\HolidayPlanner\NationalHolidays.json";
            
            string json = File.ReadAllText(jsonFilePath);
            
            List<DateTime> jsonResult = JsonConvert.DeserializeObject<List<DateTime>>(json);

            return jsonResult;
        }
    }
}
