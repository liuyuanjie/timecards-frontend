using System;
using System.Collections.Generic;
using System.ComponentModel;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Infrastructure.Model.Response;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Timecards
{
    public class BWSearchTimecardsCommand : ISearchTimecardsCommand
    {
        private BackgroundWorker _backgroundWorker;

        private readonly ITimecardsServices _timecardsServices;

        public BWSearchTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void SearchAsync(QueryTimecardsRequest queryTimecardsRequest,
            Action<ResponseBase<List<SearchTimecardsResult>>> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
                callbackProcess((ResponseBase<List<SearchTimecardsResult>>) e.Result);
            _backgroundWorker.RunWorkerAsync(queryTimecardsRequest);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queryTimecardsRequest = (QueryTimecardsRequest) e.Argument;
            e.Result = _timecardsServices.SearchTimecards(queryTimecardsRequest);
        }
    }
}