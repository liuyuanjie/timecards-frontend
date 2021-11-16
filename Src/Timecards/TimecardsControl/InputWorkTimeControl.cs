using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using System.Linq;
using Timecards.Infrastructure.Model;
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
            labelWorkDate.Text = inputWorkTime.GetWorkDateDisplayText();
            InputWorkTime.InitialTimecards = PopulateTimecards;
            InputWorkTime.SaveTimecards = SaveTimecards;
        }

        private TimecardsDataSource SaveTimecards()
        {
            var dataSource =
                new TimecardsDataSource(InputWorkTime.TimecardsDate)
                {
                    Items = new List<Item>
                    {
                        new Item(InputWorkTime.TimecardsDate,
                            numericUpDownWorkTime0.Value, textBoxNote0.Text),
                        new Item(InputWorkTime.TimecardsDate.AddDays(1),
                            numericUpDownWorkTime1.Value,
                            textBoxNote1.Text),
                        new Item(InputWorkTime.TimecardsDate.AddDays(2),
                            numericUpDownWorkTime2.Value,
                            textBoxNote2.Text),
                        new Item(InputWorkTime.TimecardsDate.AddDays(3),
                            numericUpDownWorkTime3.Value,
                            textBoxNote3.Text),
                        new Item(InputWorkTime.TimecardsDate.AddDays(4),
                            numericUpDownWorkTime4.Value,
                            textBoxNote4.Text),
                        new Item(InputWorkTime.TimecardsDate.AddDays(5),
                            numericUpDownWorkTime5.Value,
                            textBoxNote5.Text),
                        new Item(InputWorkTime.TimecardsDate.AddDays(6),
                            numericUpDownWorkTime6.Value,
                            textBoxNote6.Text)
                    }
                };

            return dataSource;
        }

        private void PopulateTimecards(TimecardsDataSource timecardsDataSource)
        {
            var items = timecardsDataSource.Items.OrderBy(x => x.WorkDay).ToList();
            for (int itemIndex = 0; itemIndex < items.Count; itemIndex++)
            {
                var workHour = panelInput.Controls.FindControl(GetWorkTimeControl(itemIndex));
                workHour.TextChanged += UpdateTotalHours;
                workHour.Text = items[itemIndex].Hour.ToString(CultureInfo.InvariantCulture);

                var workNote = panelInput.Controls.FindControl(GetWorkNoteControl(itemIndex));
                workNote.Text = items[itemIndex].Note;

                var workDay = panelInput.Controls.FindControl(GetDisplayMonthControl(itemIndex));
                workDay.Text = InputWorkTime.TimecardsDate.AddDays(itemIndex).Day.ToString("00");
            }

            labelSaveStatues.Text = InputWorkTime.StatusType != null
                ? InputWorkTime.StatusType.ToString().ToUpper()
                : "UNSAVED";
            labelTotalHour.Text = items.Sum(x => x.Hour).ToString("F1");

            FreezeInputWorkTime();
        }

        private void UpdateTotalHours(object sender, EventArgs e)
        {
            var totalHour = 0.0;
            for (int i = 0; i < Constant.DaysInWeek; i++)
            {
                totalHour += double.Parse(panelInput.Controls.FindControl(GetWorkTimeControl(i)).Text);
            }

            labelTotalHour.Text = totalHour.ToString("F1");
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            InputWorkTime.RemoveTimecards(this);
        }

        private void buttonNote_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Constant.DaysInWeek; i++)
            {
                var workNote = panelInput.Controls.FindControl(GetWorkNoteControl(i));
                workNote.Visible = true;
            }
        }

        private void FreezeInputWorkTime()
        {
            var disableChange = InputWorkTime.StatusType == StatusType.Submitted ||
                    InputWorkTime.StatusType == StatusType.Approved ||
                    InputWorkTime.StatusType == StatusType.Denied;
            
            if (!disableChange) return;
            
            for (int itemIndex = 0; itemIndex < Constant.DaysInWeek; itemIndex++)
            {
                var workHour = panelInput.Controls.FindControl(GetWorkTimeControl(itemIndex));
                workHour.Enabled = false;

                var workNote = panelInput.Controls.FindControl(GetWorkNoteControl(itemIndex));
                workNote.Enabled = false;
            }

            buttonDelete.Visible = false;
            buttonNote.Visible = false;
        }
        
        private static string GetDisplayMonthControl(int itemIndex) => $"labelMonth{itemIndex}";
        private static string GetWorkNoteControl(int itemIndex) => $"textBoxNote{itemIndex}";
        private static string GetWorkTimeControl(int itemIndex) => $"numericUpDownWorkTime{itemIndex}";
    }
}