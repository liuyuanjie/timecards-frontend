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
        public Timecards(Guid userId, Guid projectId, DateTime timecardsDate)
        {
            UserId = userId;
            ProjectId = projectId;
            TimecardsDate = timecardsDate;
        }

        public Timecards(Guid? timecardsId, Guid userId, Guid projectId, DateTime timecardsDate)
            : this(userId, projectId, timecardsDate)
        {
            TimecardsId = timecardsId;
        }

        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public DateTime TimecardsDate { get; set; }
        public Guid? TimecardsId { get; set; }
        public List<ItemcardsItem> Items { get; set; }
    }

    public class ItemcardsItem
    {
        public ItemcardsItem(DateTime workDay, decimal hour, string note)
        {
            WorkDay = workDay;
            Hour = hour;
            Note = note;
        }

        public DateTime WorkDay { get; set; }
        public decimal Hour { get; set; }
        public string Note { get; set; }
    }
}