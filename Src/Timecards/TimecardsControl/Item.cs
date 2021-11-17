using System;

namespace TimecardsControl
{
    public class Item
    {
        public Item(DateTime workDay, decimal hour, string note)
        {
            WorkDay = workDay;
            Note = note;
            Hour = hour;
        }

        public DateTime WorkDay { get; }
        public string Note { get; }
        public decimal Hour { get; }
    }
}