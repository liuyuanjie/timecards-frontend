using System;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services.Impl
{
    public class AccountService : ServiceBase, IAccountService
    {
        const string IdentityTokenEndPoint = "account/register";

        private readonly IApiRequestFactory _apiRequestFactory;

        public AccountService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void RegisterAsync(RegisterRequest registerRequest,
            Action<ResponseBase<RegisterResult>> callbackProcessHandler)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(registerRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<RegisterResult>(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildAsyncResponseResult(response)); },
                Method.POST.ToString());
        }

        public ResponseBase<RegisterResult> Register(RegisterRequest registerRequest)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(registerRequest);

            var registerResponseResult = _apiRequestFactory.CreateClient().Execute<RegisterResult>(request);

            return BuildAsyncResponseResult(registerResponseResult);
        }
    }
}