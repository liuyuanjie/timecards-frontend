using System;
using System.Collections.Generic;
using RestSharp;
using Timecards.Application;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

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
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddHeader("Authorization", $"Bearer {TokenStore.Login.Token}");

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<List<Project>>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
            }, Method.GET.ToString());
        }
    }
}
