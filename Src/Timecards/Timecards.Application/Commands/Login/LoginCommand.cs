using System;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;

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
            _identityService.AsyncLogin(loginRequest, (loginResponse) => callbackProcess(loginResponse));
        }
    }
}