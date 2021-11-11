using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace TimecardsControl
{
    public partial class InputWorkTime : UserControl
    {
        public DateTime TimecardsDate { get; set; }

        public InputWorkTime(string projectName, DateTime selectDate)
        {
            InitializeComponent();
            this.labelProject.Text = projectName;
            TimecardsDate = GetFirstDayOfWeek(selectDate);
            this.labelWorkDate.Text = GetWorkTimeDisplay();
        }

        private string GetWorkTimeDisplay()
        {
            return $"{TimecardsDate.ToString("d")}~{TimecardsDate.AddDays(7).ToString("d")}";
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
    }
}