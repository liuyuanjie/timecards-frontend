using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Timecards
{
    public interface IDeclineTimecardsCommand
    {
        void DeclineTimecardsAsync(BatchTimecardsRequest declineTimecardsRequest, Action<ResponseState> callbackProcess);
    }
}