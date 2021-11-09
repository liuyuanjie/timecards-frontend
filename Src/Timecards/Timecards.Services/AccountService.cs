﻿using System;
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

        public void AsyncRegister(RegisterRequest registerRequest, Action callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(registerRequest);
            _apiRequestFactory.CreateClient().ExecuteAsyncPost(request, (response, e) =>
            {
                callbackProcessHandler.Invoke();
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

            if (!loginResponse.ResponseState.IsSuccess)
            {
                loginResponse.RequestFailedState = JsonConvert.DeserializeObject<RequestFailedState>(response.Content);
            }

            return loginResponse;
        }
    }
}
