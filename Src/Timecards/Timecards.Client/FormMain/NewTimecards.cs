using System;
using System.Windows.Forms;
using Timecards.Application;
using TimecardsControl;

namespace Timecards.Client
{
    public partial class FormMain
    {
        private void AddEmptyInputWorkTime()
        {
            var projectId = new Guid(comboBoxProject.SelectedValue.ToString());
            if (_inputWorkTimeSource.HasExistingProject(projectId))
            {
                MessageBox.Show("The same project has added.", "Invalid Project", MessageBoxButtons.OK);
                return;
            }

            var inputWorkTime = new InputWorkTime(AccountStore.Account.AccountId,
                projectId)
            {
                ProjectName = comboBoxProject.Text,
                TimecardsDate = dateTimeWorkDate.Value.ConvertToUTCDate()
            };

            AddInputWorkTime(inputWorkTime, new TimecardsDataSource(inputWorkTime.TimecardsDate));
        }
    }
}