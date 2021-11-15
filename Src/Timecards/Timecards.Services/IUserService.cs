using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public interface IUserService
    {
        void GetUserAsync(UserRequest userRequest, Action<ResponseBase<List<UserResult>>> callbackProcessHandler);
        ResponseBase<List<UserResult>> GetUser(UserRequest userRequest);
    }
}