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
        private readonly IDeleteTimecardsCommand _deleteTimecardsCommand;

        private List<InputWorkTime> _inputWorkTimes;

        public FormMain(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();

            _getProjectsCommand = new BWGetProjectsCommand(apiRequestFactory);
            _queryTimecardsCommand = new BWQueryTimecardsCommand(apiRequestFactory);
            _saveTimecardsCommand = new BWSaveTimecardsCommand(apiRequestFactory);
            _deleteTimecardsCommand = new BWDeleteTimecardsCommand(apiRequestFactory);

            _inputWorkTimes = new List<InputWorkTime>();

            InitialData();
        }

        private void InitialData()
        {
            InitialMyProfile();
            GetProjects();

            var utcToday = DateTime.Today.ConvertToUTCDate();
            GetTimecardsOfDate(utcToday);
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
                UserId = AccountStore.Account.AccountId,
                ProjectId = new Guid(comboBoxProject.SelectedValue.ToString()),
                ProjectName = comboBoxProject.Text,
                TimecardsDate = dateTimeWorkDate.Value.Date.AddHours(8)
            };

            var inputWorkTimeControl = AddInputWorkTimeControl(inputWorkTime);
            inputWorkTimeControl.InputWorkTime.InitialTimecards(new TimecardsDataSource
            {
                TimecardsDate = inputWorkTime.TimecardsDate
            });
        }

        private InputWorkTimeControl AddInputWorkTimeControl(InputWorkTime inputWorkTime)
        {
            var inputWorkTimeControl = AddInputWorkTime(inputWorkTime);
            inputWorkTimeControl.InputWorkTime.RemoveTimecards = (control) =>
                RemoveTimecards(inputWorkTimeControl.InputWorkTime, control);

            return inputWorkTimeControl;
        }

        private void RemoveTimecards(InputWorkTime inputWorkTime, InputWorkTimeControl control)
        {
            if (!inputWorkTime.TimecardsId.HasValue)
            {
                RemoveInputWorkTime(inputWorkTime, control);
                return;
            }

            _deleteTimecardsCommand.DeleteTimecardsAsync(new DeleteTimecardsRequest()
                {
                    TimecardsId = inputWorkTime.TimecardsId.Value
                },
                responseState =>
                {
                    if (responseState.IsSuccess)
                    {
                        RemoveInputWorkTime(inputWorkTime, control);
                    }

                    MessageBox.Show(
                        responseState.IsSuccess ? "Delete Successfully!" : "Delete Failed",
                        "Delete",
                        MessageBoxButtons.OK);
                });
        }

        private InputWorkTimeControl AddInputWorkTime(InputWorkTime inputWorkTime)
        {
            _inputWorkTimes.Add(inputWorkTime);
            var inputWorkTimeControl = splitContainerWorkTime.Panel2.Controls.AddInputWorkTimeControl(inputWorkTime);

            return inputWorkTimeControl;
        }

        private void RemoveInputWorkTime(InputWorkTime inputWorkTime, InputWorkTimeControl control)
        {
            _inputWorkTimes.Remove(inputWorkTime);
            splitContainerWorkTime.Panel2.Controls.RemoveInputWorkTimeControl(control);
        }

        private void SaveTimecards()
        {
            if (!_inputWorkTimes.Any())
            {
                MessageBox.Show(
                    "Nothing need to be save!",
                    "Save",
                    MessageBoxButtons.OK);
                return;
            }

            var saveTimecardsRequest = new SaveTimecardsRequest()
            {
                Timecardses = _inputWorkTimes.Select(x => new Infrastructure.Model.Timecards
                {
                    ProjectId = x.ProjectId,
                    UserId = AccountStore.Account.AccountId,
                    TimecardsId = x.TimecardsId,
                    TimecardsDate = x.TimecardsDate.ConvertToUTCDate(),
                    Items = x.SaveTimecards.Invoke().Items.Select(t => new ItemcardsItem()
                    {
                        WorkDay = t.WorkDay.ConvertToUTCDate(),
                        Hour = t.Hour,
                        Note = t.Note
                    }).ToList()
                }).ToList()
            };

            _saveTimecardsCommand.SaveTimecardsAsync(saveTimecardsRequest,
                responseState =>
                {
                    if (responseState.IsSuccess)
                    {
                        MessageBox.Show(
                            "Save Successfully!",
                            "Save",
                            MessageBoxButtons.OK);
                        GetTimecardsOfDate(saveTimecardsRequest.Timecardses.First().TimecardsDate);
                    }
                    else
                    {
                        MessageBox.Show(
                            "Save Failed",
                            "Save",
                            MessageBoxButtons.OK);
                    }
                });
        }

        private void dateTimeWorkDate_ValueChanged(object sender, EventArgs e)
        {
            GetTimecardsOfDate(dateTimeWorkDate.Value.ConvertToUTCDate());
        }

        private void GetTimecardsOfDate(DateTime date)
        {
            var queryTimecardsRequest = new QueryTimecardsRequest()
            {
                UserId = AccountStore.Account.AccountId,
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
            _inputWorkTimes.Clear();

            responseResult.ResponseResult.ForEach(x =>
            {
                var inputWorkTime = new InputWorkTime()
                {
                    UserId = x.UserId,
                    ProjectId = x.ProjectId,
                    TimecardsId = x.TimecardsId,
                    ProjectName = comboBoxProject.Items.Cast<Project>().First(p => p.ProjectId == x.ProjectId).Name,
                    TimecardsDate = x.TimecardsDate.Date
                };
                var inputWorkTimeControl = AddInputWorkTimeControl(inputWorkTime);
                var dataSource = new TimecardsDataSource()
                {
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

        private void comboBoxProject_SelectedValueChanged(object sender, EventArgs e)
        {
            var comboBox = (ComboBox) sender;
            buttonNew.Enabled = ((Project) comboBox.SelectedItem).ParentProjectId.HasValue;
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveTimecards();
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            throw new System.NotImplementedException();
        }
    }
}