using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace TimecardsControl
{
    public partial class InputWorkTime : UserControl
    {
        public Guid UseId { get; set; }
        public Guid ProjectId { get; set; }

        public DateTime TimecardsDate { get; set; }

        public Action<TimecardsDataSource> SaveTimecards;

        public Action<TimecardsDataSource> InitialTimecards;

        public InputWorkTime()
        {

        }

        public InputWorkTime(Guid userId, Guid projectId, string projectName, DateTime selectDate)
        {
            InitializeComponent();

            userId = userId;
            ProjectId = projectId;
            labelProject.Text = projectName;
            TimecardsDate = GetFirstDayOfWeek(selectDate);
            labelWorkDate.Text = GetWorkTimeDisplay();
            InitialTimecards = new Action<TimecardsDataSource>(PopulateTimecards); ;
        }

        private string GetWorkTimeDisplay()
        {
            return $"{TimecardsDate.ToString("M")} - {TimecardsDate.AddDays(7).ToString("M")}";
        }

        private const byte DaysInWeek = 7;

        private DateTime GetFirstDayOfWeek(DateTime day)
        {
            return day.AddDays(-(day.DayOfWeek == DayOfWeek.Sunday
                ? DaysInWeek
                : (byte)day.DayOfWeek) + 1);
        }

        private void buttonSave_Click(object sender, EventArgs e)
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

        private void PopulateTimecards(TimecardsDataSource timecardsDataSource)
        {
            UseId = timecardsDataSource.UserId;
            ProjectId = timecardsDataSource.ProjectId;
            TimecardsDate = timecardsDataSource.TimecardsDate;

            var items = timecardsDataSource.Items.OrderBy(x => x.WorkDay).ToList();

            numericUpDownWorkTime1.Value = items[0].Hour;
            textBoxNote1.Text = items[0].Note;

            numericUpDownWorkTime2.Value = items[1].Hour;
            textBoxNote2.Text = items[1].Note;

            numericUpDownWorkTime3.Value = items[2].Hour;
            textBoxNote3.Text = items[2].Note;

            numericUpDownWorkTime4.Value = items[3].Hour;
            textBoxNote4.Text = items[3].Note;

            numericUpDownWorkTime5.Value = items[4].Hour;
            textBoxNote5.Text = items[4].Note;

            numericUpDownWorkTime6.Value = items[5].Hour;
            textBoxNote6.Text = items[5].Note;

            numericUpDownWorkTime7.Value = items[6].Hour;
            textBoxNote7.Text = items[6].Note;
        }
    }
}