using System;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model
{
    public class SaveTimecardsRequest
    {
        public List<Timecards> Timecardses { get; set; }
    }

    public class Timecards
    {
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public Guid? TimecardsId { get; set; }
        public List<ItemcardsItem> Items { get; set; }
    }
    
    public class ItemcardsItem
    {
        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
    }
}