using System;
using RestSharp;
using Timecards.Application;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services.Extensions;

namespace Timecards.Services.Impl
{
    public class AccountService : ServiceBase, IAccountService
    {
        const string IdentityTokenEndPoint = "account";

        private readonly IApiRequestFactory _apiRequestFactory;

        public AccountService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void RegisterAsync(RegisterRequest registerRequest,
            Action<ResponseBase<RegisterResult>> callbackProcessHandler)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/register", Method.POST);
            request.AddJsonBody(registerRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<RegisterResult>(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildAsyncResponseResult(response)); },
                Method.POST.ToString());
        }

        public ResponseBase<RegisterResult> Register(RegisterRequest registerRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/register", Method.POST);
            request.AddJsonBody(registerRequest);

            var registerResponseResult = _apiRequestFactory.CreateClient().Execute<RegisterResult>(request);

            return BuildAsyncResponseResult(registerResponseResult);
        }

        public ResponseBase<AccountResult> GetAccount(UserRequest userRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/{userRequest.AccountId}", Method.GET);
            request.AddAuthorizationHeader();

            var accountResponseResult = _apiRequestFactory.CreateClient().Execute<AccountResult>(request);

            return BuildAsyncResponseResult(accountResponseResult);
        }

        public void GetAccountAsync(UserRequest userRequest, Action<ResponseBase<AccountResult>> callbackProcessHandler)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/{userRequest.AccountId}", Method.GET);
            request.AddAuthorizationHeader();

            _apiRequestFactory.CreateClient().ExecuteAsyncGet<AccountResult>(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildAsyncResponseResult(response)); },
                Method.GET.ToString());
        }
    }
}