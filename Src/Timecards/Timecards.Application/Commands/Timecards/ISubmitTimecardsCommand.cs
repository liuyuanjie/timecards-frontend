using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Timecards
{
    public interface ISubmitTimecardsCommand
    {
        void SubmitTimecardsAsync(BatchTimecardsRequest submitTimecardsRequest, Action<ResponseState> callbackProcess);
    }
}