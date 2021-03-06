using System;
using System.Collections.Generic;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services.Extensions;

namespace Timecards.Services.Impl
{
    public class UserService : ServiceBase, IUserService
    {
        const string IdentityTokenEndPoint = "users";

        private readonly IApiRequestFactory _apiRequestFactory;

        public UserService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void GetUserAsync(UserRequest userRequest, Action<ResponseBase<List<UserResult>>> callbackProcessHandler)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddAuthorizationHeader();
            request.AddQueryParameter(nameof(UserRequest.Email), userRequest.Email);

            _apiRequestFactory.CreateClient().ExecuteAsyncGet<List<UserResult>>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
            }, Method.GET.ToString());
        }
        
        public ResponseBase<List<UserResult>> GetUser(UserRequest userRequest)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddAuthorizationHeader();
            request.AddQueryParameter(nameof(UserRequest.Email), userRequest.Email);

            var getUserResponseResult = _apiRequestFactory.CreateClient().Execute<List<UserResult>>(request);

            return BuildAsyncResponseResult(getUserResponseResult);
        }
    }
}
