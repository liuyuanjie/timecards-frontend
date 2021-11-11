using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace TimecardsControl
{
    public partial class InputWorkTime : UserControl
    {
        public DateTime TimecardsDate { get; set; }
        public Guid ProjectId { get; set; }

        public Action<TimecardsDataSource> SaveTimecards;

        public InputWorkTime(Guid projectId, string projectName, DateTime selectDate)
        {
            InitializeComponent();
            ProjectId = projectId;
            labelProject.Text = projectName;
            TimecardsDate = GetFirstDayOfWeek(selectDate);
            labelWorkDate.Text = GetWorkTimeDisplay();
        }

        private string GetWorkTimeDisplay()
        {
            return $"{TimecardsDate.ToString("M")}~{TimecardsDate.AddDays(7).ToString("M")}";
        }

        private const byte DaysInWeek = 7;

        /// <summary>
        /// Monday is set to the first day in week.
        /// </summary>
        /// <param name="day">The day.</param>
        private DateTime GetFirstDayOfWeek(DateTime day)
        {
            return day.AddDays(-(day.DayOfWeek == DayOfWeek.Sunday
                ? DaysInWeek
                : (byte) day.DayOfWeek) + 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var dataSource = new TimecardsDataSource()
            {
                ProjectId = ProjectId,
                TimecardsDate = TimecardsDate,
                Items = new List<Item>
                {
                    new Item()
                    {
                        WorkDay = TimecardsDate,
                        Hour = numericUpDownWorkTime1.Value,
                        Note = textBoxNote1.Text
                    },
                    new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(1),
                        Hour = numericUpDownWorkTime2.Value,
                        Note = textBoxNote2.Text
                    },
                    new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(2),
                        Hour = numericUpDownWorkTime3.Value,
                        Note = textBoxNote3.Text
                    },
                    new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(3),
                        Hour = numericUpDownWorkTime4.Value,
                        Note = textBoxNote4.Text
                    },
                    new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(4),
                        Hour = numericUpDownWorkTime5.Value,
                        Note = textBoxNote5.Text
                    },
                    new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(5),
                        Hour = numericUpDownWorkTime6.Value,
                        Note = textBoxNote6.Text
                    },
                    new Item()
                    {
                        WorkDay = TimecardsDate.AddDays(6),
                        Hour = numericUpDownWorkTime7.Value,
                        Note = textBoxNote7.Text
                    }
                }
            };

            SaveTimecards(dataSource);
        }
    }
}