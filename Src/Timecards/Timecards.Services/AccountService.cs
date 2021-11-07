using System;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApiRequestFactory _apiRequestFactory;

        public AccountService(ApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public IRestResponse Register(RegisterRequest registerRequest)
        {
            RestRequest request = new RestRequest("identity/token", Method.POST);
            request.AddJsonBody(registerRequest);
            var response = _apiRequestFactory.CreateClient().Execute(request);

            return response;
        }
    }
}
