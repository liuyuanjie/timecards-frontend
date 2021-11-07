﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Permissions;

namespace Timecards.Infrastructure.Model
{
    public class ResponseBase
    {
        public ResponseState ResponseState { get; set; }
        public RequestFailedState RequestFailedState { get; set; }
    }

    public class ResponseState
    {
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public Exception ErrorException { get; set; }
        public bool IsSuccess => StatusCode == HttpStatusCode.OK;
    }

    public class RequestFailedState
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<KeyValuePair<string, string>> SubErrorMessage { get; set; }
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
