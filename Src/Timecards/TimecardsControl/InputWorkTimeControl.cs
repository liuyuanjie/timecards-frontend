using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Linq;
using TimecardsControl.Extensions;

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
            InputWorkTime.InitialTimecards = PopulateTimecards;
            InputWorkTime.SaveTimecards = SaveTimecards;
        }

        private string GetWorkTimeDisplay()
        {
            return
                $"{InputWorkTime.TimecardsDate.ToString("M")} - {InputWorkTime.TimecardsDate.AddDays(7).ToString("M")}";
        }

        private TimecardsDataSource SaveTimecards()
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
                        Note = textBoxNote1.Text,
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

            return dataSource;
        }

        private void PopulateTimecards(TimecardsDataSource timecardsDataSource)
        {
            var items = timecardsDataSource.Items.OrderBy(x => x.WorkDay).ToList();
            for (int itemIndex = 1; itemIndex <= items.Count; itemIndex++)
            {
                var workHour = panelInput.Controls.FindControl($"numericUpDownWorkTime{itemIndex}");
                workHour.TextChanged += UpdateTotalHours;
                workHour.Text = items[itemIndex - 1].Hour.ToString();
                var workNote = panelInput.Controls.FindControl($"textBoxNote{itemIndex}");
                workNote.Text = items[itemIndex - 1].Note;
                var workDay = panelInput.Controls.FindControl($"labelMonth{itemIndex}");
                workDay.Text = InputWorkTime.TimecardsDate.AddDays(itemIndex - 1).Day.ToString("00");
            }

            labelSaveStatues.Text = InputWorkTime.TimecardsId != null ? "SAVED" : "UNSAVED";
            labelTotalHour.Text = items.Sum(x => x.Hour).ToString("F1");
        }

        private void UpdateTotalHours(object sender, EventArgs e)
        {
            var totalHour = 0.0;
            for (int i = 1; i <= Constant.DaysInWeek; i++)
            {
                totalHour += double.Parse(panelInput.Controls.FindControl($"numericUpDownWorkTime{i}").Text);
            }

            labelTotalHour.Text = totalHour.ToString("F1");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            InputWorkTime.RemoveTimecards(this);
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= Constant.DaysInWeek; i++)
            {
                var workNote = panelInput.Controls.FindControl($"textBoxNote{i}");
                workNote.Visible = true;
            }
        }
    }
}