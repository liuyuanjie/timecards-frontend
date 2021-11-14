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
            InputWorkTime.InitialTimecards = PopulateTimecards;
        }

        private string GetWorkTimeDisplay()
        {
            return
                $"{InputWorkTime.TimecardsDate.ToString("M")} - {InputWorkTime.TimecardsDate.AddDays(7).ToString("M")}";
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

            InputWorkTime.SaveTimecards(dataSource);
        }

        private void PopulateTimecards(TimecardsDataSource timecardsDataSource)
        {
            var items = timecardsDataSource.Items.OrderBy(x => x.WorkDay).ToList();
            for (int i = 1; i <= items.Count; i++)
            {
                var workHour = panelInput.Controls.Find($"numericUpDownWorkTime{i}", true)[0];
                workHour.TextChanged += UpdateTotalHours;
                workHour.Text = items[i - 1].Hour.ToString();
                var workNote = panelInput.Controls.Find($"textBoxNote{i}", true)[0];
                workNote.Text = items[i - 1].Note;
                var workDay = panelInput.Controls.Find($"labelMonth{i}", true)[0];
                workDay.Text = InputWorkTime.TimecardsDate.AddDays(i - 1).Day.ToString("00");
            }

            labelSaveStatues.Text = InputWorkTime.TimecardsId != null ? "SAVED" : "UNSAVED";
            labelTotalHour.Text = items.Sum(x => x.Hour).ToString("F1");
        }

        private void UpdateTotalHours(object sender, EventArgs e)
        {
            var totalHour = 0.0;
            for (int i = 1; i <= 7; i++)
            {
                totalHour += double.Parse(panelInput.Controls.Find($"numericUpDownWorkTime{i}", true)[0].Text);
            }

            labelTotalHour.Text = totalHour.ToString("F1");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            InputWorkTime.RemoveTimecards(this);
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            for (int i = 1; i <= 7; i++)
            {
                var workNote = panelInput.Controls.Find($"textBoxNote{i}", true)[0];
                workNote.Visible = true;
            }
        }
    }
}