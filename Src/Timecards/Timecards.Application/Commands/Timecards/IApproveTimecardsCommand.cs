using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Timecards
{
    public interface IApproveTimecardsCommand
    {
        void ApproveTimecardsAsync(BatchTimecardsRequest approveTimecardsRequest, Action<ResponseState> callbackProcess);
    }
}