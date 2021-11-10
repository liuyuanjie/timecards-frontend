using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Project
{
    public interface IGetProjectsCommand
    {
        void GetProjectsAsync(Action<ResponseBase<ProjectResult>> callbackProcess);
    }
}