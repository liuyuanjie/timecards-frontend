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
        void AsyncRegister(RegisterRequest registerRequest, Action callbackProcessHandler);
    }
}
