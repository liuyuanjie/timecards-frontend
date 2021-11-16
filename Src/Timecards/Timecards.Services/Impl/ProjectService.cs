using System;
using System.Collections.Generic;
using RestSharp;
using Timecards.Application;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services.Extensions;

namespace Timecards.Services.Impl
{
    public class ProjectService : ServiceBase, IProjectService
    {
        const string IdentityTokenEndPoint = "projects";

        private readonly IApiRequestFactory _apiRequestFactory;

        public ProjectService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void GetProjectsAsync(Action<ResponseBase<List<Project>>> callbackProcessHandler)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddAuthorizationHeader();

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<List<Project>>(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildAsyncResponseResult(response)); },
                Method.GET.ToString());
        }

        public ResponseBase<List<Project>> GetProjects()
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddAuthorizationHeader();

            return BuildAsyncResponseResult(_apiRequestFactory.CreateClient().Execute<List<Project>>(request));
        }
    }
}