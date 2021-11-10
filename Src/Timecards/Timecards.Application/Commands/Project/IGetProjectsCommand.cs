using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Project
{
    public interface IGetProjectsCommand
    {
        void GetProjectsAsync(ProjectRequest loginRequest, Action<ResponseBase<ProjectResult>> callbackProcess);
    }
}