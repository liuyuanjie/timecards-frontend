using System;
using System.Collections.Generic;
using System.ComponentModel;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Timecards
{
    public class BWQueryTimecardsCommand : IQueryTimecardsCommand
    {
        private BackgroundWorker _backgroundWorker;

        private readonly ITimecardsServices _timecardsServices;

        public BWQueryTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void GetAsync(QueryTimecardsRequest queryTimecardsRequest,
            Action<ResponseBase<List<TimecardsResult>>> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;

            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
            {
                callbackProcess((ResponseBase<List<TimecardsResult>>) e.Result);
            };
            _backgroundWorker.RunWorkerAsync(queryTimecardsRequest);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queryTimecardsRequest = (QueryTimecardsRequest) e.Argument;
            e.Result = _timecardsServices.GetTimecards(queryTimecardsRequest);
        }
    }
}