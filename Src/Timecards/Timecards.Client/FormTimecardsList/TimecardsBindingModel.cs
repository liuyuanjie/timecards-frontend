using System;
using System.Collections.Generic;
using System.Linq;
using Timecards.Infrastructure.Model;

namespace Timecards.Client
{
    public class TimecardsBindingModel
    {
        public Guid TimecardsId { get; set; }
        public string ProjectName { get; set; }
        public Guid UserId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public decimal Hour { get; set; }

        public static List<TimecardsBindingModel> ConvertTo(List<TimecardsResult> timecards)
        {
            return timecards
                .Where(x => x.StatusType == StatusType.Submitted)
                .Select(x => new TimecardsBindingModel
                {
                    TimecardsId = x.TimecardsId,
                    ProjectName = x.ProjectName,
                    UserId = x.UserId,
                    StartDate = x.Items.Min(t => t.WorkDay).Date.ToString("M"),
                    EndDate = x.Items.Max(t => t.WorkDay).Date.ToString("M"),
                    Hour = x.Items.Sum(t => t.Hour)
                }).ToList();
        }
    }
}