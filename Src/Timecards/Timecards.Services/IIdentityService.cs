using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public interface IIdentityService
    {
        void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcessHandler);

        ResponseBase<LoginResult> Login(LoginRequest loginRequest);
    }
}
