using System;
using System.Collections.Generic;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public interface ITimecardsServices
    {
        void GetTimecardsAsync(QueryTimecardsRequest queryTimecardsRequest, Action<ResponseBase<List<TimecardsResult>>> callbackProcessHandler);
        ResponseBase<List<TimecardsResult>> GetTimecards(QueryTimecardsRequest queryTimecardsRequest);
        void SaveTimecardsAsync(SaveTimecardsRequest saveTimecardsRequest, Action<ResponseState> callbackProcessHandler);
    }
}