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
        private readonly IAccountService _accountService;

        public LoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _identityService = new IdentityService(apiRequestFactory);
            _userService = new UserService(apiRequestFactory);
            _accountService = new AccountService(apiRequestFactory);
        }

        public void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcess)
        {
            var reCreatedCallbackProcess = new Action<ResponseBase<LoginResult>>(StoreToken());
            reCreatedCallbackProcess += GetLogin(callbackProcess);
            _identityService.LoginAsync(loginRequest, (loginResponse) => reCreatedCallbackProcess(loginResponse));
        }

        private Action<ResponseBase<LoginResult>> StoreToken()
        {
            return responseResult =>
            {
                if (responseResult.ResponseState.IsSuccess)
                {
                    TokenStore.Login = new LoginResult { Token = responseResult.ResponseResult.Token };
                }
            };
        }

        private Action<ResponseBase<LoginResult>> GetLogin(Action<ResponseBase<LoginResult>> callbackProcess)
        {
            return responseResult =>
            {
                if (responseResult.ResponseState.IsSuccess)
                {
                    GetLoginAccount(callbackProcess, responseResult);
                }
                else
                {
                    callbackProcess(new ResponseBase<LoginResult>()
                    {
                        ResponseState = responseResult.ResponseState
                    });
                }
            };
        }

        private void GetLoginAccount(Action<ResponseBase<LoginResult>> callbackProcess, ResponseBase<LoginResult> responseResult)
        {
            _accountService.GetAccountAsync(new UserRequest
            {
                AccountId = responseResult.ResponseResult.AccountId
            }, userResponseResult =>
            {
                if (responseResult.ResponseState.IsSuccess)
                {
                    AccountStore.Account = userResponseResult.ResponseResult;
                }

                callbackProcess(new ResponseBase<LoginResult>()
                {
                    ResponseState = userResponseResult.ResponseState,
                });
            });
        }
    }
}