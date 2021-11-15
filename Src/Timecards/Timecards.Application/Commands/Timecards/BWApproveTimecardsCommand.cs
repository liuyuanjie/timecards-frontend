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
    public class ApproveTimecardsCommand : IApproveTimecardsCommand
    {
        private readonly ITimecardsServices _timecardsServices;
        private BackgroundWorker _backgroundWorker;

        public ApproveTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void ApproveTimecardsAsync(BatchTimecardsRequest approveTimecardsRequest,
            Action<ResponseState> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted += (sender, e) =>
                callbackProcess((ResponseState) e.Result);
            _backgroundWorker.RunWorkerAsync(approveTimecardsRequest);
        }
        
        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var queryTimecardsRequest = (BatchTimecardsRequest) e.Argument;
            e.Result = _timecardsServices.Approveimecards(queryTimecardsRequest);
        }
    }
}