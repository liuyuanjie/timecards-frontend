using System;
using System.Security.Principal;
using System.Threading;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IdentityService _identityService;
        public LoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _identityService = new IdentityService(apiRequestFactory);
        }

        public void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcess)
        {
            _identityService.LoginAsync(loginRequest, (loginResponse) => callbackProcess(loginResponse));
        }
    }
}