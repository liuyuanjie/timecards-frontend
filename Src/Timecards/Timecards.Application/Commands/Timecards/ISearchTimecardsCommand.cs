using System;
using System.Collections.Generic;
using Timecards.Infrastructure.Model;
using Timecards.Infrastructure.Model.Response;

namespace Timecards.Application.Commands.Timecards
{
    public interface ISearchTimecardsCommand
    {
        void SearchAsync(QueryTimecardsRequest queryTimecardsRequest,
            Action<ResponseBase<List<SearchTimecardsResult>>> callbackProcess);
    }
}