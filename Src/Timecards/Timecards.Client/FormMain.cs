using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Timecards.Application;
using Timecards.Application.Commands;
using Timecards.Application.Commands.Project;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using TimecardsControl;
using TimecardsControl.Extensions;

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

            _getProjectsCommand = new BWGetProjectsCommand(apiRequestFactory);
            _queryTimecardsCommand = new BWQueryTimecardsCommand(apiRequestFactory);
            _saveTimecardsCommand = new SaveTimecardsCommand(apiRequestFactory);

            InitialData();
        }

        private void InitialData()
        {
            InitialMyProfile();

            GetProjects();
            GetTimecardsOfDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0,
                DateTimeKind.Utc));
        }

        private void InitialMyProfile()
        {
            labelFirstName.Text = AccountStore.Account.FirstName;
            labelLastName.Text = AccountStore.Account.LastName;
            labelEmail.Text = AccountStore.Account.Email;
            LabelUserName.Text = AccountStore.Account.UserName;
            labelRole.Text = AccountStore.Account.Role;
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
            var inputWorkTime = new InputWorkTime()
            {
                UserId = AccountStore.Account.UserId,
                ProjectId = new Guid(comboBoxProject.SelectedValue.ToString()),
                ProjectName = comboBoxProject.Text,
                TimecardsDate = dateTimeWorkDate.Value.Date.AddHours(8)
            };
            AddInputWorkTimeControl(inputWorkTime);
        }

        private InputWorkTimeControl AddInputWorkTimeControl(InputWorkTime inputWorkTime)
        {
            var inputWorkTimeControl = splitContainerWorkTime.Panel2.Controls.AddInputWorkTimeControl(inputWorkTime);
            inputWorkTimeControl.InputWorkTime.SaveTimecards = SaveTimecards;
            inputWorkTimeControl.InputWorkTime.RemoveTimecards =
                splitContainerWorkTime.Panel2.Controls.RemoveInputWorkTimeControl;

            return inputWorkTimeControl;
        }

        private void SaveTimecards(TimecardsDataSource timecardsDataSource)
        {
            var saveTimecardsRequest = new SaveTimecardsRequest()
            {
                ProjectId = timecardsDataSource.ProjectId,
                UserId = AccountStore.Account.UserId,
                TimecardsDate = timecardsDataSource.TimecardsDate.ToUniversalTime(),
                Items = timecardsDataSource.Items.Select(x => new ItemcardsItem()
                {
                    WorkDay = x.WorkDay.ToUniversalTime(),
                    Hour = x.Hour,
                    Note = x.Note
                }).ToList()
            };

            _saveTimecardsCommand.SaveTimecardsAsync(saveTimecardsRequest,
                loginResponse => CallbackProcess(loginResponse));
        }

        private void CallbackProcess(ResponseState responseState)
        {
            MessageBox.Show(
                responseState.IsSuccess ? "Save Successfully!" : "Save Failed",
                "Save",
                MessageBoxButtons.OK);
        }

        private void dateTimeWorkDate_ValueChanged(object sender, EventArgs e)
        {
            GetTimecardsOfDate(dateTimeWorkDate.Value.Date.AddHours(8));
        }

        private void GetTimecardsOfDate(DateTime date)
        {
            var queryTimecardsRequest = new QueryTimecardsRequest()
            {
                UserId = AccountStore.Account.UserId,
                TimecardsDate = date.ToUniversalTime()
            };

            _queryTimecardsCommand.GetAsync(queryTimecardsRequest,
                responseResult => CallbackProcessPopulate(responseResult));
        }

        private void CallbackProcessPopulate(ResponseBase<List<TimecardsResult>> responseResult)
        {
            if (responseResult.ResponseState.IsSuccess)
            {
                PopulateWorkTimes(responseResult);
            }
            else
            {
                MessageBox.Show(
                    responseResult.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Load",
                    MessageBoxButtons.OK);
            }
        }

        private void PopulateWorkTimes(ResponseBase<List<TimecardsResult>> responseResult)
        {
            splitContainerWorkTime.Panel2.Controls.RemoveAllInputWorkTimes();

            responseResult.ResponseResult.ForEach(x =>
            {
                var inputWorkTime = new InputWorkTime()
                {
                    UserId = x.UserId,
                    ProjectId =x.ProjectId,
                    ProjectName = comboBoxProject.Items.Cast<Project>().First(p => p.ProjectId == x.ProjectId).Name,
                    TimecardsDate = x.TimecardsDate.Date
                };
                var inputWorkTimeControl = AddInputWorkTimeControl(inputWorkTime);
                var dataSource = new TimecardsDataSource()
                {
                    UserId = x.UserId,
                    ProjectId = x.ProjectId,
                    TimecardsDate = x.TimecardsDate,
                    Items = x.Items.Select(s => new Item
                    {
                        Hour = s.Hour,
                        Note = s.Note,
                        WorkDay = s.WorkDay
                    }).ToList()
                };
                inputWorkTimeControl.InputWorkTime.InitialTimecards(dataSource);
            });
        }
    }
}