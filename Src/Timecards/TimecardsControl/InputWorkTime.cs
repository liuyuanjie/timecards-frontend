using System;
using System.Collections.Generic;

namespace TimecardsControl
{
    public class InputWorkTime
    {
        public Func<TimecardsDataSource> SaveTimecards { get; set; }
        public Action<TimecardsDataSource> InitialTimecards { get; set; }
        public Action<InputWorkTimeControl> RemoveTimecards { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid? TimecardsId { get; set; }
        public string ProjectName { get; set; }

        private DateTime _dateTime;
        public DateTime TimecardsDate
        {
            set => _dateTime = value;
            get => GetFirstDayOfWeek(_dateTime);
        }

        private DateTime GetFirstDayOfWeek(DateTime day)
        {
            return day.AddDays(-(day.DayOfWeek == DayOfWeek.Sunday
                ? Constant.DaysInWeek
                : (byte) day.DayOfWeek) + 1);
        }
    }
}