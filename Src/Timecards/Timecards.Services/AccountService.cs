using System;
using Newtonsoft.Json;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        const string IdentityTokenEndPoint = "account/register";

        private readonly IApiRequestFactory _apiRequestFactory;

        public AccountService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void AsyncRegister(RegisterRequest registerRequest, Action<ResponseBase<RegisterResult>> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(registerRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<RegisterResult>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
            }, Method.POST.ToString());
        }

    }
}
