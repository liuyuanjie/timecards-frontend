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

            InitializaData();
        }

        private void InitializaData()
        {
            labelFirstName.Text = AccountStore.Account.FirstName;
            labelLastName.Text = AccountStore.Account.LastName;
            labelEmail.Text = AccountStore.Account.Email;
            LabelUserName.Text = AccountStore.Account.UserName;
            labelRole.Text = AccountStore.Account.Role;

            GetProjects();
        }

        public void GetProjects()
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

        private void FormMain_Load(object sender, EventArgs e)
        {

        }
    }
}
