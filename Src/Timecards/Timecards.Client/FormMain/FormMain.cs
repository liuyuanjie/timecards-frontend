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
        private IGetProjectsCommand _getProjectsCommand;
        private IQueryTimecardsCommand _queryTimecardsCommand;
        private ISaveTimecardsCommand _saveTimecardsCommand;
        private IDeleteTimecardsCommand _deleteTimecardsCommand;
        private ISubmitTimecardsCommand _submitTimecardsCommand;
        private InputWorkTimeSource _inputWorkTimeSource;

        public FormMain(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
            InitialCommand(apiRequestFactory);
            InitialData();
            InitialButtonCommand();
        }

        private void InitialButtonCommand()
        {
            buttonNew.Click += (s, e) => AddEmptyInputWorkTime();
            buttonSave.Click += (s, e) => SaveTimecards();
            buttonSubmit.Click += (s, e) => SubmitTimecards();
            
            LoadTimecardsEvent += ClearInputWorkTimes;
            PopulateTimecardsEvent += UpdateSubmitButtonState;
            PopulateTimecardsEvent += UpdateSaveButtonState;
            PopulateTimecardsEvent += UpdateNewButtonState;
        }

        private void InitialCommand(IApiRequestFactory apiRequestFactory)
        {
            _getProjectsCommand = new BWGetProjectsCommand(apiRequestFactory);
            _queryTimecardsCommand = new BWQueryTimecardsCommand(apiRequestFactory);
            _saveTimecardsCommand = new BWSaveTimecardsCommand(apiRequestFactory);
            _deleteTimecardsCommand = new BWDeleteTimecardsCommand(apiRequestFactory);
            _submitTimecardsCommand = new BWSubmitTimecardsCommand(apiRequestFactory);
        }

        private void InitialData()
        {
            _inputWorkTimeSource = new InputWorkTimeSource();

            LoadMyProfile();
            LoadProjects();
            LoadTimecardsOfDay(DateTime.Today.ConvertToUTCDate());
        }

        private void LoadMyProfile()
        {
            labelFirstName.Text = AccountStore.Account.FirstName;
            labelLastName.Text = AccountStore.Account.LastName;
            labelEmail.Text = AccountStore.Account.Email;
            LabelUserName.Text = AccountStore.Account.UserName;
            labelRole.Text = AccountStore.Account.Role;
        }

        private void dateTimeWorkDate_ValueChanged(object sender, EventArgs e)
        {
            LoadTimecardsOfDay(((DateTimePicker) sender).Value.ConvertToUTCDate());
        }

        private void comboBoxProject_SelectedValueChanged(object sender, EventArgs e)
        {
            UpdateNewButtonState();
        }

        private void UpdateTotalWorkHour(decimal hours)
        {
            labelWorkHours.Text = hours.ToString("F1");
        }

        private void RemoveInputWorkTime(InputWorkTime inputWorkTime, InputWorkTimeControl control)
        {
            _inputWorkTimeSource.Remove(inputWorkTime);
            splitContainerWorkTime.Panel2.Controls.RemoveInputWorkTimeControl(control);

            UpdateSaveButtonState();
        }

        private void AddInputWorkTime(InputWorkTime inputWorkTime, TimecardsDataSource dataSource)
        {
            _inputWorkTimeSource.Add(inputWorkTime);

            var inputWorkTimeControl = splitContainerWorkTime.Panel2.Controls.AddInputWorkTimeControl(inputWorkTime);
            inputWorkTimeControl.InputWorkTime.RemoveTimecards = (control) =>
                RemoveTimecards(inputWorkTimeControl.InputWorkTime, control);
            inputWorkTimeControl.InputWorkTime.InitialTimecards(dataSource);

            UpdateSaveButtonState();
        }

        private void UpdateSaveButtonState()
        {
            buttonSave.Enabled = _inputWorkTimeSource.AllowSave();
        }

        private void UpdateSubmitButtonState()
        {
            buttonSubmit.Enabled = _inputWorkTimeSource.AllowSubmit();
        }

        private void UpdateNewButtonState()
        {
            var validProject = ((Project) comboBoxProject.SelectedItem)?.ParentProjectId.HasValue ?? false;
            buttonNew.Enabled = validProject && _inputWorkTimeSource.AllowNew();
        }
    }
}