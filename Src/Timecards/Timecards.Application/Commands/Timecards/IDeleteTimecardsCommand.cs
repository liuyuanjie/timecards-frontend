using System;
using Timecards.Infrastructure.Model;

namespace Timecards.Application.Commands.Timecards
{
    public interface IDeleteTimecardsCommand
    {
        void DeleteTimecardsAsync(DeleteTimecardsRequest deleteTimecardsRequest, Action<ResponseState> callbackProcess);
    }
}