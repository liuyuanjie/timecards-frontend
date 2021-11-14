using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public interface IAccountService
    {
        void RegisterAsync(RegisterRequest registerRequest, Action<ResponseBase<RegisterResult>> callbackProcessHandler);
        ResponseBase<RegisterResult> Register(RegisterRequest registerRequest);
    }
}
