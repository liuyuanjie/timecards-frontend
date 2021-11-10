using System;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Login
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IdentityService _identityService;
        private readonly IUserService _userService;

        public LoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _identityService = new IdentityService(apiRequestFactory);
            _userService = new UserService(apiRequestFactory);
        }

        public void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcess)
        {
            callbackProcess += StoreToken();
            _identityService.LoginAsync(loginRequest, (loginResponse) => callbackProcess(loginResponse));
        }

        private Action<ResponseBase<LoginResult>> StoreToken()
        {
            return responseResult => { TokenStore.Login.Token = responseResult.ResponseResult.Token; };
        }

        private Action<ResponseBase<LoginResult>> GetLogin()
        {
            return responseResult =>
            {
                _userService.GetUserAsync(new UserRequest
                {
                    Email = responseResult.ResponseResult.Email
                }, userResponseResult =>
                {
                    AccountStore.Account = userResponseResult.ResponseResult;
                });
            };
        }
    }
}