using System;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Project
{
    public class GetProjectsCommand : IGetProjectsCommand
    {
        private readonly ProjectService _projectService;

        public GetProjectsCommand(IApiRequestFactory apiRequestFactory)
        {
            _projectService = new ProjectService(apiRequestFactory);
        }

        public void GetProjectsAsync(ProjectRequest projectRequest, Action<ResponseBase<ProjectResult>> callbackProcess)
        {
            _projectService.GetProjectsAsync(projectRequest, (projectResponse) => callbackProcess(projectResponse));
        }
    }
}