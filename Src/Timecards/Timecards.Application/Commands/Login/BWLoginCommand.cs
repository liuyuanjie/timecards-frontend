using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Login
{
    public class BWLoginCommand : ILoginCommand
    {
        private readonly IdentityService _identityService;
        private readonly IAccountService _accountService;
        private BackgroundWorker _backgroundWorker;

        public BWLoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _identityService = new IdentityService(apiRequestFactory);
            _accountService = new AccountService(apiRequestFactory);
        }

        public void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;

            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
            {
                callbackProcess((ResponseBase<LoginResult>) e.Result);
            };
            _backgroundWorker.RunWorkerAsync(loginRequest);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var loginRequest = (LoginRequest) e.Argument;
            var loginResponseResult = _identityService.Login(loginRequest);
            if (!loginResponseResult.ResponseState.IsSuccess)
            {
                e.Result = loginResponseResult;
            }
            else
            {
                TokenStore.Login = loginResponseResult.ResponseResult;
                e.Result = StoreMyProfile(loginResponseResult);
            }
        }

        private ResponseBase<LoginResult> StoreMyProfile(ResponseBase<LoginResult> loginResponseResult)
        {
            var userResponseResult = GetLoginAccount(loginResponseResult.ResponseResult.AccountId);
            if (userResponseResult.ResponseState.IsSuccess)
            {
                AccountStore.Account = userResponseResult.ResponseResult;
            }

            return new ResponseBase<LoginResult>()
            {
                ResponseState = userResponseResult.ResponseState,
                ResponseResult = loginResponseResult.ResponseResult
            };
        }

        private ResponseBase<AccountResult> GetLoginAccount(Guid accountId)
        {
            var userRequest = new UserRequest
            {
                AccountId = accountId
            };
            var userResponseResult = _accountService.GetAccount(userRequest);
            return userResponseResult;
        }
    }
}