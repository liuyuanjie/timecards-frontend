using System;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model
{
    public class SaveTimecardsRequest
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public List<Item> Items { get; set; }
    }
    
    public class Item
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
    }
}