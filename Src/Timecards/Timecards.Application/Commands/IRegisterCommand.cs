using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands
{
    public interface IRegisterCommand
    {
        void RegisterAsync(RegisterRequest registerRequest, Action callbackProcess);
    }
}