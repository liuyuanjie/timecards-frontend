using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Timecards.Application;
using Timecards.Application.Commands.Project;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Client
{
    public partial class FormMain : Form
    {
        private readonly IGetProjectsCommand _getProjectsCommand;

        public FormMain(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();

            _getProjectsCommand = new GetProjectsCommand(apiRequestFactory);

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
            _getProjectsCommand.GetProjectsAsync((projectResponse) => CallbackProcess(projectResponse));
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
            var count = splitContainerWorkTime.Panel2.Controls
                .Cast<Control>()
                .Count(x => x.GetType() == typeof(TimecardsControl.InputWorkTime));
            var inputWorkTime = new TimecardsControl.InputWorkTime(this.comboBoxProject.SelectedText, this.dateTimeWorkDate.Value)
            {
                Location = new Point(3, 3 + count * 100)
            };
            this.splitContainerWorkTime.Panel2.Controls.Add(inputWorkTime);
        }
    }
}