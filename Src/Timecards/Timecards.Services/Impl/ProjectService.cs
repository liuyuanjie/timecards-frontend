using System;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services.Impl
{
    public class ProjectService : ServiceBase, IProjectService
    {
        const string IdentityTokenEndPoint = "Project/register";

        private readonly IApiRequestFactory _apiRequestFactory;

        public ProjectService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void GetProjectsAsync(ProjectRequest projectRequest, Action<ResponseBase<ProjectResult>> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(projectRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<ProjectResult>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
            }, Method.POST.ToString());
        }

    }
}
