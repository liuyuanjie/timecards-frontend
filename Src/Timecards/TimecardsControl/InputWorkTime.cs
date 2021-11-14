using System;

namespace TimecardsControl
{
    public class InputWorkTime
    {
        public Action<TimecardsDataSource> SaveTimecards { get; set; }
        public Action<TimecardsDataSource> InitialTimecards { get; set; }
        public Action<InputWorkTimeControl> RemoveTimecards { get; set; }
        public Guid UserId { get; set; }
        public Guid ProjectId { get; set; }

        private DateTime _dateTime;
        public DateTime TimecardsDate
        {
            set => _dateTime = value;
            get => GetFirstDayOfWeek(_dateTime);
        }

        public string ProjectName { get; set; }

        private const byte DaysInWeek = 7;
        private DateTime GetFirstDayOfWeek(DateTime day)
        {
            return day.AddDays(-(day.DayOfWeek == DayOfWeek.Sunday
                ? DaysInWeek
                : (byte) day.DayOfWeek) + 1);
        }
    }
}