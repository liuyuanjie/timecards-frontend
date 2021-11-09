using System;
using Timecards.Application.Commands;
using Timecards.Infrastructure.Model;
using Timecards.Services;

namespace Timecards.Infrastructure
{
    public class RegisterCommand : IRegisterCommand
    {
        private readonly IAccountService _accountService;
        public RegisterCommand(IApiRequestFactory apiRequestFactory)
        {
            _accountService = new AccountService(apiRequestFactory);
        }

        public void RegisterAsync(RegisterRequest registerRequest, Action<RegisterResponse> callbackProcess)
        {
            _accountService.AsyncRegister(registerRequest, callbackProcess);
        }
    }
}