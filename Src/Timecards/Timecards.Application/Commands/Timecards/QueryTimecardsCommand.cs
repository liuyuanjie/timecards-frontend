using System;
using System.Collections.Generic;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Timecards
{
    public class QueryTimecardsCommand : IQueryTimecardsCommand
    {
        private readonly ITimecardsServices _timecardsServices;

        public QueryTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void LoginAsync(QueryTimecardsRequest queryTimecardsRequest,
            Action<ResponseBase<List<TimecardsResult>>> callbackProcess)
        {
            _timecardsServices.GetTimecardsAsync(queryTimecardsRequest, callbackProcess);
        }
    }
}