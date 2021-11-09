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
            _apiRequestFactory.CreateClient().ExecuteAsyncPost<LoginResponseResult>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildLoginResponse(response));
            }, Method.POST.ToString());
        }

        private static LoginResponse BuildLoginResponse(IRestResponse<LoginResponseResult> response)
        {
            var registerResponse = new LoginResponse
            {
                ResponseState = new ResponseState
                {
                    StatusCode = response.StatusCode,
                }
            };

            if (registerResponse.ResponseState.IsSuccess)
            {
                registerResponse.ResponseResult = response.Data;
            }
            else
            {
                if (response.ErrorException != null)
                {
                    registerResponse.ResponseState.ResponseStateMessage = new ResponseStateMessage
                    {
                        ErrorCode = "RequestFailed",
                        ErrorMessage = response.ErrorMessage
                    };
                }
                else
                {
                    registerResponse.ResponseState.ResponseStateMessage =
                        JsonConvert.DeserializeObject<ResponseStateMessage>(response.Content);
                }
            }

            return registerResponse;
        }
    }
}