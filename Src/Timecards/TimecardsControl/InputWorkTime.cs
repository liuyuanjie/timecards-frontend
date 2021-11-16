using System;
using Timecards.Infrastructure.Model;

namespace TimecardsControl
{
    public class InputWorkTime
    {
        public InputWorkTime(Guid userId, Guid projectId)
        {
            UserId = userId;
            ProjectId = projectId;
        }

        public InputWorkTime(Guid timecardsId, Guid userId, Guid projectId, StatusType statusType)
            : this(userId, projectId)
        {
            TimecardsId = timecardsId;
            StatusType = statusType;
        }

        public Func<TimecardsDataSource> SaveTimecards { get; set; }
        public Action<TimecardsDataSource> InitialTimecards { get; set; }
        public Action<InputWorkTimeControl> RemoveTimecards { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TimecardsId { get; set; }
        public StatusType? StatusType { get; set; }
        public string ProjectName { get; set; }

        private DateTime _dateTime;
        public DateTime TimecardsDate
        {
            set => _dateTime = value;
            get => GetFirstDayOfWeek(_dateTime);
        }

        public string GetWorkDateDisplayText()
        {
            return
                $"{TimecardsDate.ToString("M")} - {TimecardsDate.AddDays(Constant.DaysInWeek).ToString("M")}";
        }

        private DateTime GetFirstDayOfWeek(DateTime day)
        {
            return day.AddDays(-(day.DayOfWeek == DayOfWeek.Sunday
                ? Constant.DaysInWeek
                : (byte) day.DayOfWeek) + 1);
        }
    }
}