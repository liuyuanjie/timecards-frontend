using System;
using System.ComponentModel;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands
{
    public class BWDeclineTimecardsCommand : IDeclineTimecardsCommand
    {
        private readonly ITimecardsServices _timecardsServices;
        private BackgroundWorker _backgroundWorker;

        public BWDeclineTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void DeclineTimecardsAsync(BatchTimecardsRequest declineTimecardsRequest,
            Action<ResponseState> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
                callbackProcess((ResponseState) e.Result);
            _backgroundWorker.RunWorkerAsync(declineTimecardsRequest);
        }
        
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queryTimecardsRequest = (BatchTimecardsRequest) e.Argument;
            e.Result = _timecardsServices.DeclineTimecards(queryTimecardsRequest);
        }
    }
}