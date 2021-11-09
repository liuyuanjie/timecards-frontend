using System;
using Newtonsoft.Json;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class AccountService : IAccountService
    {
        const string IdentityTokenEndPoint = "account/register";

        private readonly IApiRequestFactory _apiRequestFactory;

        public AccountService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void AsyncRegister(RegisterRequest registerRequest, Action<RegisterResponse> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(registerRequest);
            _apiRequestFactory.CreateClient().ExecuteAsyncPost(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildRegisterResponse(response));
            }, Method.POST.ToString());
        }

        private static RegisterResponse BuildRegisterResponse(IRestResponse response)
        {
            var registerResponse = new RegisterResponse
            {
                ResponseState = new ResponseState
                {
                    StatusCode = response.StatusCode,
                    ErrorException = response.ErrorException,
                    ErrorMessage = response.ErrorMessage
                }
            };

            if (!registerResponse.ResponseState.IsSuccess)
            {
                registerResponse.RequestFailedState = JsonConvert.DeserializeObject<RequestFailedState>(response.Content);
            }

            return registerResponse;
        }
    }
}
