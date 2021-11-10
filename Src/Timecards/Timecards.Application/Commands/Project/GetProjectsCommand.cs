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

        public void GetProjectsAsync(Action<ResponseBase<ProjectResult>> callbackProcess)
        {

            _projectService.GetProjectsAsync((projectResponse) => callbackProcess(new ResponseBase<ProjectResult>
            {
                ResponseState = projectResponse.ResponseState,
                ResponseResult = new ProjectResult { Projects = projectResponse.ResponseResult }
            }));
        }
    }
}