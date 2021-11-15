using System;
using System.Collections.Generic;
using System.ComponentModel;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands
{
    public class BWDeleteTimecardsCommand : IDeleteTimecardsCommand
    {
        private readonly ITimecardsServices _timecardsServices;
        private BackgroundWorker _backgroundWorker;

        public BWDeleteTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void DeleteTimecardsAsync(DeleteTimecardsRequest deleteTimecardsRequest,
            Action<ResponseState> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
                callbackProcess((ResponseState) e.Result);
            _backgroundWorker.RunWorkerAsync(deleteTimecardsRequest);
        }
        
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queryTimecardsRequest = (DeleteTimecardsRequest) e.Argument;
            e.Result = _timecardsServices.DeleteTimecards(queryTimecardsRequest);
        }
    }
}