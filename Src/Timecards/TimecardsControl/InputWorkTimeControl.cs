using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;

namespace TimecardsControl
{
    public partial class InputWorkTimeControl : UserControl
    {
        public readonly InputWorkTime InputWorkTime;

        public InputWorkTimeControl(InputWorkTime inputWorkTime)
        {
            InitializeComponent();
            InputWorkTime = inputWorkTime;
            labelProject.Text = inputWorkTime.ProjectName;
            labelWorkDate.Text = GetWorkTimeDisplay();
        }

        private string GetWorkTimeDisplay()
        {
            return $"{InputWorkTime.TimecardsDate.ToString("M")} - {InputWorkTime.TimecardsDate.AddDays(7).ToString("M")}";
        }
        
        private void buttonSave_Click(object sender, EventArgs e)
        {
            var dataSource = new TimecardsDataSource()
            {
                ProjectId = InputWorkTime.ProjectId,
                TimecardsDate = InputWorkTime.TimecardsDate,
                Items = new List<Item>
                {
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate,
                        Hour = numericUpDownWorkTime1.Value,
                        Note = textBoxNote1.Text
                    },
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate.AddDays(1),
                        Hour = numericUpDownWorkTime2.Value,
                        Note = textBoxNote2.Text
                    },
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate.AddDays(2),
                        Hour = numericUpDownWorkTime3.Value,
                        Note = textBoxNote3.Text
                    },
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate.AddDays(3),
                        Hour = numericUpDownWorkTime4.Value,
                        Note = textBoxNote4.Text
                    },
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate.AddDays(4),
                        Hour = numericUpDownWorkTime5.Value,
                        Note = textBoxNote5.Text
                    },
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate.AddDays(5),
                        Hour = numericUpDownWorkTime6.Value,
                        Note = textBoxNote6.Text
                    },
                    new Item()
                    {
                        WorkDay = InputWorkTime.TimecardsDate.AddDays(6),
                        Hour = numericUpDownWorkTime7.Value,
                        Note = textBoxNote7.Text
                    }
                }
            };

            InputWorkTime.SaveTimecards(dataSource);
        }

        private void PopulateTimecards(TimecardsDataSource timecardsDataSource)
        {
            InputWorkTime.UserId = timecardsDataSource.UserId;
            InputWorkTime.ProjectId = timecardsDataSource.ProjectId;
            InputWorkTime.TimecardsDate = timecardsDataSource.TimecardsDate;

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

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            InputWorkTime.RemoveTimecards(this);
        }
    }
}