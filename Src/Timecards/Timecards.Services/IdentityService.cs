using System;
using Newtonsoft.Json;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class IdentityService : ServiceBase, IIdentityService
    {
        const string IdentityTokenEndPoint = "identity/token";

        private readonly IApiRequestFactory _apiRequestFactory;

        public IdentityService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void AsyncLogin(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(loginRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<LoginResult>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
            }, Method.POST.ToString());
        }
    }
}