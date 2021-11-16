using System.Windows.Forms;
using Timecards.Infrastructure.Model;

namespace Timecards.Client
{
    public partial class FormMain
    {
        private void LoadProjects()
        {
            _getProjectsCommand.GetProjectsAsync(projectResponse => GetProjectCallbackProcess(projectResponse));
        }

        private void GetProjectCallbackProcess(ResponseBase<ProjectResult> projectResponse)
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
    }
}