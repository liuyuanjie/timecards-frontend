using System;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services.Impl
{
    public class IdentityService : ServiceBase, IIdentityService
    {
        const string IdentityTokenEndPoint = "identity/token";

        private readonly IApiRequestFactory _apiRequestFactory;

        public IdentityService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(loginRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<LoginResult>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(loginRequest, response));
            }, Method.POST.ToString());
        }

        private ResponseBase<LoginResult> BuildAsyncResponseResult(LoginRequest loginRequest, IRestResponse<LoginResult> response)
        {
            response.Data.Email = loginRequest.Email;
            return base.BuildAsyncResponseResult(response);
        }
    }
}