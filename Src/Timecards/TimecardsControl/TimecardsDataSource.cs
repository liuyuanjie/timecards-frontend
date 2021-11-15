using System;
using System.Collections.Generic;
using System.Linq;

namespace TimecardsControl
{
    public class TimecardsDataSource
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public string ProjectName { get; set; }
        public DateTime TimecardsDate { get; set; }

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
                    items.Add(new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(i)
                    });
                }

                return items;
            }
        }

        public Guid? TimecardsId { get; set; }
    }

    public class Item
    {
        public DateTime WorkDay { get; set; }
        public string Note { get; set; }
        public decimal Hour { get; set; }
    }
}