using System;
using System.Collections.Generic;

namespace DesignCrowd.Holidays
{
    public class HolidaysAggregator : IHolidaysFactory
    {
        private List<IHolidaysFactory> _holidayFactories;

        public HolidaysAggregator(List<IHolidaysFactory> holidayFactories)
        {
            this._holidayFactories = holidayFactories;
        }

        public IList<DateTime> CreateHolidays(DateTime start, DateTime end)
        {
            var holidays = new List<DateTime>();
            foreach (var factory in _holidayFactories)
            {
                holidays.AddRange(factory.CreateHolidays(start, end));
            }
            return holidays;
        }
    }
}
