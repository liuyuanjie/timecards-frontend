using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Timecards.Client.FormTimecardsList
{
    public partial class FormTimecardsList
    {
        private void InitialTimecardsDataGridView()
        {
            dataGridViewTimecards.AutoGenerateColumns = false;
            dataGridViewTimecards.ReadOnly = false;
            dataGridViewTimecards.Enabled = true;

            DataGridViewCheckBoxColumn selectColumn = new DataGridViewCheckBoxColumn();
            selectColumn.ValueType = typeof(bool);
            selectColumn.Name = "columnSelect";
            selectColumn.HeaderText = "Select";
            selectColumn.ReadOnly = false;
            dataGridViewTimecards.Columns.Add(selectColumn);

            DataGridViewColumn timecardsColumn = new DataGridViewTextBoxColumn();
            timecardsColumn.ValueType = typeof(Guid);
            timecardsColumn.Name = "columnId";
            timecardsColumn.DataPropertyName = "TimecardsId";
            timecardsColumn.Visible = false;
            dataGridViewTimecards.Columns.Add(timecardsColumn);

            DataGridViewColumn projectColumn = new DataGridViewTextBoxColumn();
            projectColumn.ValueType = typeof(string);
            projectColumn.Name = "Project";
            projectColumn.DataPropertyName = "ProjectName";
            dataGridViewTimecards.Columns.Add(projectColumn);

            DataGridViewColumn userColumn = new DataGridViewTextBoxColumn();
            userColumn.ValueType = typeof(string);
            userColumn.Name = "Staff";
            userColumn.DataPropertyName = "UserId";
            dataGridViewTimecards.Columns.Add(userColumn);

            DataGridViewColumn inputStartDateColumn = new DataGridViewTextBoxColumn();
            inputStartDateColumn.ValueType = typeof(DateTime);
            inputStartDateColumn.Name = "Start Date";
            inputStartDateColumn.DataPropertyName = "StartDate";
            dataGridViewTimecards.Columns.Add(inputStartDateColumn);

            DataGridViewColumn inputEndDateColumn = new DataGridViewTextBoxColumn();
            inputEndDateColumn.ValueType = typeof(DateTime);
            inputEndDateColumn.Name = "End Date";
            inputEndDateColumn.DataPropertyName = "EndDate";
            dataGridViewTimecards.Columns.Add(inputEndDateColumn);

            DataGridViewColumn hourColumn = new DataGridViewTextBoxColumn();
            hourColumn.ValueType = typeof(decimal);
            hourColumn.Name = "Hour";
            hourColumn.DataPropertyName = "Hour";
            dataGridViewTimecards.Columns.Add(hourColumn);
        }

        private List<Guid> GetSelectedTimecardsIds()
        {
            var timecardsIds = dataGridViewTimecards.Rows
                .Cast<DataGridViewRow>()
                .Select(x => new
                {
                    IsChecked = (bool?) x.Cells["columnSelect"].Value ?? false,
                    Id = new Guid(x.Cells["columnId"].Value.ToString())
                })
                .Where(x => x.IsChecked)
                .Select(x => x.Id)
                .ToList();

            return timecardsIds;
        }
    }
}