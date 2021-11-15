using System;
using System.ComponentModel;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands
{
    public class BWSubmitTimecardsCommand : ISubmitTimecardsCommand
    {
        private readonly ITimecardsServices _timecardsServices;
        private BackgroundWorker _backgroundWorker;

        public BWSubmitTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void SubmitTimecardsAsync(BatchTimecardsRequest submitTimecardsRequest,
            Action<ResponseState> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
                callbackProcess((ResponseState) e.Result);
            _backgroundWorker.RunWorkerAsync(submitTimecardsRequest);
        }
        
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queryTimecardsRequest = (BatchTimecardsRequest) e.Argument;
            e.Result = _timecardsServices.SubmitTimecards(queryTimecardsRequest);
        }
    }
}