using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Timecards.Application;
using Timecards.Application.Commands;
using Timecards.Application.Commands.Project;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using TimecardsControl;

namespace Timecards.Client
{
    public partial class FormMain : Form
    {
        private readonly IGetProjectsCommand _getProjectsCommand;
        private readonly IQueryTimecardsCommand _queryTimecardsCommand;
        private readonly ISaveTimecardsCommand _saveTimecardsCommand;

        public FormMain(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();

            _getProjectsCommand = new GetProjectsCommand(apiRequestFactory);
            _queryTimecardsCommand = new QueryTimecardsCommand(apiRequestFactory);
            _saveTimecardsCommand = new SaveTimecardsCommand(apiRequestFactory);

            InitialData();
        }

        private void InitialData()
        {
            labelFirstName.Text = AccountStore.Account.FirstName;
            labelLastName.Text = AccountStore.Account.LastName;
            labelEmail.Text = AccountStore.Account.Email;
            LabelUserName.Text = AccountStore.Account.UserName;
            labelRole.Text = AccountStore.Account.Role;

            GetProjects();
        }

        private void GetProjects()
        {
            _getProjectsCommand.GetProjectsAsync(projectResponse => CallbackProcess(projectResponse));
        }

        private void CallbackProcess(ResponseBase<ProjectResult> projectResponse)
        {
            if (projectResponse.ResponseState.IsSuccess)
            {
                var dataSource = projectResponse.ResponseResult.GetProjectTree();
                comboBoxProject.DataSource = dataSource;
                comboBoxProject.DisplayMember = nameof(Project.Name);
                comboBoxProject.ValueMember = nameof(Project.ProjectId);
            }
            else
            {
                MessageBox.Show(projectResponse.ResponseState.ResponseStateMessage.OutputResponseMessage());
            }
        }

        private void buttonNew_Click(object sender, EventArgs e)
        {
            var timecardsControlHigh = 105;
            var defaultPosition = 3;
            var count = splitContainerWorkTime.Panel2.Controls
                .Cast<Control>()
                .Count(x => x.GetType() == typeof(InputWorkTime));

            var inputWorkTime = new InputWorkTime(
                new Guid(comboBoxProject.SelectedValue.ToString()),
                comboBoxProject.Text,
                dateTimeWorkDate.Value)
            {
                Location = new Point(defaultPosition, defaultPosition + count * timecardsControlHigh)
            };
            splitContainerWorkTime.Panel2.Controls.Add(inputWorkTime);
            inputWorkTime.SaveTimecards = SaveTimecards;
        }

        private void SaveTimecards(TimecardsDataSource timecardsDataSource)
        {
            var saveTimecardsRequest = new SaveTimecardsRequest()
            {
                ProjectId = timecardsDataSource.ProjectId,
                UserId = AccountStore.Account.UserId,
                TimecardsDate = timecardsDataSource.TimecardsDate,
                Items = timecardsDataSource.Items.Select(x => new Infrastructure.Model.Item()
                {
                    WorkDay = x.WorkDay,
                    Hour = x.Hour,
                    Note = x.Note
                }).ToList()
            };

            _saveTimecardsCommand.SaveTimecardsAsync(saveTimecardsRequest,
                loginResponse => CallbackProcess(loginResponse));
        }

        private void CallbackProcess(ResponseState responseState)
        {
            if (responseState.IsSuccess)
            {
                MessageBox.Show(
                    "Save Successfully!",
                    "Save",
                    MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(
                    responseState.ResponseStateMessage.OutputResponseMessage(),
                    "Login Failed",
                    MessageBoxButtons.OK);
            }
        }
    }
}