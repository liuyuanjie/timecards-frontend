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
            selectColumn.Width = 50;
            dataGridViewTimecards.Columns.Add(selectColumn);

            DataGridViewColumn timecardsColumn = new DataGridViewTextBoxColumn();
            timecardsColumn.ValueType = typeof(Guid);
            timecardsColumn.Name = "keyId";
            timecardsColumn.DataPropertyName = "TimecardsId";
            timecardsColumn.Visible = false;
            dataGridViewTimecards.Columns.Add(timecardsColumn);

            DataGridViewColumn projectColumn = new DataGridViewTextBoxColumn();
            projectColumn.ValueType = typeof(string);
            projectColumn.Name = "Project";
            projectColumn.DataPropertyName = "ProjectName";
            projectColumn.Width = 200;
            dataGridViewTimecards.Columns.Add(projectColumn);

            DataGridViewColumn userFistNameColumn = new DataGridViewTextBoxColumn();
            userFistNameColumn.ValueType = typeof(string);
            userFistNameColumn.Name = "Full Name";
            userFistNameColumn.DataPropertyName = "FullName";
            userFistNameColumn.Width = 150;
            dataGridViewTimecards.Columns.Add(userFistNameColumn);

            DataGridViewColumn userRoleColumn = new DataGridViewTextBoxColumn();
            userRoleColumn.ValueType = typeof(string);
            userRoleColumn.Name = "Role";
            userRoleColumn.DataPropertyName = "Role";
            dataGridViewTimecards.Columns.Add(userRoleColumn);

            DataGridViewColumn inputStartDateColumn = new DataGridViewTextBoxColumn();
            inputStartDateColumn.ValueType = typeof(DateTime);
            inputStartDateColumn.Name = "Start Date";
            inputStartDateColumn.DataPropertyName = "DisplayStartDate";
            inputStartDateColumn.Width = 200;
            dataGridViewTimecards.Columns.Add(inputStartDateColumn);

            DataGridViewColumn inputEndDateColumn = new DataGridViewTextBoxColumn();
            inputEndDateColumn.ValueType = typeof(DateTime);
            inputEndDateColumn.Name = "End Date";
            inputEndDateColumn.DataPropertyName = "DisplayEndDate";
            inputEndDateColumn.Width = 200;
            dataGridViewTimecards.Columns.Add(inputEndDateColumn);

            DataGridViewColumn hourColumn = new DataGridViewTextBoxColumn();
            hourColumn.ValueType = typeof(decimal);
            hourColumn.Name = "Total Hour";
            hourColumn.DataPropertyName = "TotalHour";
            dataGridViewTimecards.Columns.Add(hourColumn);
        }

        private List<Guid> GetSelectedTimecardsIds()
        {
            var timecardsIds = dataGridViewTimecards.Rows
                .Cast<DataGridViewRow>()
                .Select(x => new
                {
                    IsChecked = (bool?) x.Cells["columnSelect"].Value ?? false,
                    Id = new Guid(x.Cells["keyId"].Value.ToString())
                })
                .Where(x => x.IsChecked)
                .Select(x => x.Id)
                .ToList();

            return timecardsIds;
        }
    }
}