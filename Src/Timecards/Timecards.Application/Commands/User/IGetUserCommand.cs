using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.User
{
    public interface IGetUserCommand
    {
        void LoginAsync(UserRequest userRequest, Action<ResponseBase<UserResult>> callbackProcess);
    }
}