using System;
using Newtonsoft.Json;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class IdentityService : IIdentityService
    {
        const string IdentityTokenEndPoint = "identity/token";

        private readonly IApiRequestFactory _apiRequestFactory;

        public IdentityService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void AsyncLogin(LoginRequest loginRequest, Action<LoginResponse> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(loginRequest);
            _apiRequestFactory.CreateClient().ExecuteAsyncPost(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(LoginResponse(response));
            }, Method.POST.ToString());
        }

        private static LoginResponse LoginResponse(IRestResponse response)
        {
            var loginResponse = new LoginResponse
            {
                ResponseState = new ResponseState
                {
                    StatusCode = response.StatusCode,
                    ErrorException = response.ErrorException,
                    ErrorMessage = response.ErrorMessage
                }
            };

            if (loginResponse.ResponseState.IsSuccess)
            {
                loginResponse.Token = response.Content;
            }
            else
            {
                loginResponse.RequestFailedState = JsonConvert.DeserializeObject<RequestFailedState>(response.Content);
            }

            return loginResponse;
        }
    }
}