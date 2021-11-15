using System;
using System.ComponentModel;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Project
{
    public class BWGetProjectsCommand : IGetProjectsCommand
    {
        private readonly BackgroundWorker _backgroundWorker;

        private readonly IProjectService _projectService;

        public BWGetProjectsCommand(IApiRequestFactory apiRequestFactory)
        {
            _projectService = new ProjectService(apiRequestFactory);
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var getProjectResponse = _projectService.GetProjects();
            e.Result = new ResponseBase<ProjectResult>
            {
                ResponseState = getProjectResponse.ResponseState,
                ResponseResult = new ProjectResult {Projects = getProjectResponse.ResponseResult}
            };
        }

        public void GetProjectsAsync(Action<ResponseBase<ProjectResult>> callbackProcess)
        {
            _backgroundWorker.RunWorkerCompleted +=
                (sender, e) => callbackProcess((ResponseBase<ProjectResult>) e.Result);
            _backgroundWorker.RunWorkerAsync();
        }
    }
}