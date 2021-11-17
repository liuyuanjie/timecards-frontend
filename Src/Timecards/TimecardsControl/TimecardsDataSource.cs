using System;
using System.Collections.Generic;
using System.Linq;

namespace TimecardsControl
{
    public class TimecardsDataSource
    {
        private readonly DateTime _timecardsDate;

        public TimecardsDataSource()
        {
        }
        
        public TimecardsDataSource(DateTime timecardsDate)
        {
            _timecardsDate = timecardsDate;
        }

        private List<Item> _items;
        public List<Item> Items
        {
            set { _items = value; }
            get
            {
                if (_items != null && _items.Any())
                {
                    return _items;
                }

                var items = new List<Item>();
                for (var i = 0; i < Constant.DaysInWeek; i++)
                {
                    items.Add(new Item(_timecardsDate.AddDays(i), 0, null));
                }

                return items;
            }
        }
    }
}