using System;
using System.Collections.Generic;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.User
{
    public interface IGetUserCommand
    {
        void LoginAsync(UserRequest userRequest, Action<ResponseBase<List<UserResult>>> callbackProcess);
    }
}