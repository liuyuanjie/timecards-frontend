using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Timecards
{
    public interface ISaveTimecardsCommand
    {
        void SaveTimecardsAsync(SaveTimecardsRequest saveTimecardsRequest, Action<ResponseState> callbackProcess);
    }
}