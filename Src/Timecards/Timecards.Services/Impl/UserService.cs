using System;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Services.Impl
{
    public class UserService : ServiceBase, IUserService
    {
        const string IdentityTokenEndPoint = "account/register";

        private readonly IApiRequestFactory _apiRequestFactory;

        public UserService(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void GetUserAsync(UserRequest registerRequest, Action<ResponseBase<UserResult>> callbackProcessHandler)
        {
            RestRequest request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddJsonBody(registerRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost<UserResult>(request, (response, e) =>
            {
                callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
            }, Method.POST.ToString());
        }
    }
}
