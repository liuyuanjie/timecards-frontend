using System;
using Timecards.Application.Commands;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Infrastructure
{
    public class RegisterCommand : IRegisterCommand
    {
        private readonly IAccountService _accountService;
        public RegisterCommand(IApiRequestFactory apiRequestFactory)
        {
            _accountService = new AccountService(apiRequestFactory);
        }

        public void RegisterAsync(RegisterRequest registerRequest, Action<ResponseBase<RegisterResult>> callbackProcess)
        {
            _accountService.RegisterAsync(registerRequest, callbackProcess);
        }
    }
}