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
            GetTimecardsOfDate(DateTime.Now.Date);
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
            var inputWorkTime = AnddInputWorkTime();
            inputWorkTime.SaveTimecards = SaveTimecards;
        }

        private InputWorkTime AnddInputWorkTime()
        {
            var timecardsControlHigh = 105;
            var defaultPosition = 3;
            var count = splitContainerWorkTime.Panel2.Controls
                .Cast<Control>()
                .Count(x => x.GetType() == typeof(InputWorkTime));

            var inputWorkTime = new InputWorkTime(
                AccountStore.Account.UserId,
                new Guid(comboBoxProject.SelectedValue.ToString()),
                comboBoxProject.Text,
                dateTimeWorkDate.Value.Date)
            {
                Location = new Point(defaultPosition, defaultPosition + count * timecardsControlHigh)
            };
            splitContainerWorkTime.Panel2.Controls.Add(inputWorkTime);
            return inputWorkTime;
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
                    "Save Failed",
                    "Save",
                    MessageBoxButtons.OK);
            }
        }

        private void dateTimeWorkDate_ValueChanged(object sender, EventArgs e)
        {
            GetTimecardsOfDate(dateTimeWorkDate.Value.Date);
        }

        private void GetTimecardsOfDate(DateTime date)
        {
            var queryTimecardsRequest = new QueryTimecardsRequest()
            {
                UserId = AccountStore.Account.UserId,
                TimecardsDate = date
            };

            _queryTimecardsCommand.GetAsync(queryTimecardsRequest,
                responseResult => CallbackProcessPopulate(responseResult));
        }

        private void CallbackProcessPopulate(ResponseBase<List<TimecardsResult>> responseResult)
        {
            if (responseResult.ResponseState.IsSuccess)
            {
                RemoveAllInputWorkTimes();

                responseResult.ResponseResult.ForEach(x =>
                {
                    var inputWorkTime = AnddInputWorkTime();
                    inputWorkTime.InitialTimecards(new TimecardsDataSource()
                    {
                        UserId = x.UserId,
                        ProjectId = x.ProjectId,
                        TimecardsDate = x.TimecardsDate,
                        Items = x.Items.Select(s =>
                            new TimecardsControl.Item
                            {
                                Hour = s.Hour,
                                Note = s.Note,
                                WorkDay = s.WorkDay
                            }).ToList()
                    });
                });
            }
            else
            {
                MessageBox.Show(
                    responseResult.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Load",
                    MessageBoxButtons.OK);
            }
        }

        private void RemoveAllInputWorkTimes()
        {
            var items = splitContainerWorkTime.Panel2.Controls
                .Cast<Control>()
                .Where(x => x.GetType() == typeof(InputWorkTime))
                .Cast<InputWorkTime>()
                .ToList();
            items.ForEach(x => splitContainerWorkTime.Panel2.Controls.Remove(x));
        }
    }
}