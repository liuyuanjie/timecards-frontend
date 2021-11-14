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
        private readonly IUserService _userService;
        private BackgroundWorker _backgroundWorker;

        public BWLoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _identityService = new IdentityService(apiRequestFactory);
            _userService = new UserService(apiRequestFactory);
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
                loginResponseResult.ResponseResult.Email = loginRequest.Email;
                TokenStore.Login = new LoginResult
                {
                    Token = loginResponseResult.ResponseResult.Token,
                    Email = loginRequest.Email
                };
                e.Result = StoreMyProfile(loginResponseResult);
            }
        }

        private ResponseBase<LoginResult> StoreMyProfile(ResponseBase<LoginResult> loginResponseResult)
        {
            var userResponseResult = GetLoginAccount(loginResponseResult.ResponseResult.Email);
            if (userResponseResult.ResponseState.IsSuccess)
            {
                AccountStore.Account = userResponseResult.ResponseResult.First();
            }

            return new ResponseBase<LoginResult>()
            {
                ResponseState = userResponseResult.ResponseState,
                ResponseResult = loginResponseResult.ResponseResult
            };
        }

        private ResponseBase<List<UserResult>> GetLoginAccount(string email)
        {
            var userResponseResult = _userService.GetUser(new UserRequest
            {
                Email = email
            });

            return userResponseResult;
        }
    }
}