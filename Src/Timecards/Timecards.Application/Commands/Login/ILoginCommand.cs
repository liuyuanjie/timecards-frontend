using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands
{
    public interface ILoginCommand
    {
        void LoginAsync(LoginRequest loginRequest, Action<ResponseBase<LoginResult>> callbackProcess);
    }
}