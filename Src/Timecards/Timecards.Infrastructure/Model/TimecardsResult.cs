using System;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model
{
    public class TimecardsResult
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public List<Item> Items { get; set; }
    }
}