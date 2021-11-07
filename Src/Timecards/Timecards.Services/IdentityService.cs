using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class IdentityService: IIdentityService
    {
        private readonly IApiRequestFactory _apiRequestFactory;

        public IdentityService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public LoginResponse GetToken(LoginRequest loginRequest)
        {
            RestRequest request = new RestRequest("identity/token", Method.POST);
            request.AddJsonBody(loginRequest);
            var response = _apiRequestFactory.CreateClient().Execute(request);

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