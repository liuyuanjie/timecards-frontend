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
        private readonly ApiRequestFactory _apiRequestFactory;

        public IdentityService(ApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public IRestResponse GetToken(LoginRequest loginRequest)
        {
            RestRequest request = new RestRequest("identity/token", Method.POST);
            request.AddJsonBody(loginRequest);
            var response = _apiRequestFactory.CreateClient().Execute(request);

            return response;
        }
    }
}