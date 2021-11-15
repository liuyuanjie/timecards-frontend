using System;
using System.ComponentModel;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands.Register
{
    public class BWRegisterCommand : IRegisterCommand
    {
        private readonly IAccountService _accountService;
        private BackgroundWorker _backgroundWorker;

        public BWRegisterCommand(IApiRequestFactory apiRequestFactory)
        {
            _accountService = new AccountService(apiRequestFactory);
        }

        private void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            var getRegisterResponse = _accountService.Register((RegisterRequest) e.Argument);
            e.Result = getRegisterResponse;
        }

        public void RegisterAsync(RegisterRequest registerRequest, Action<ResponseBase<RegisterResult>> callbackProcess)
        {
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerCompleted +=
                (sender, e) => callbackProcess((ResponseBase<RegisterResult>) e.Result);
            _backgroundWorker.RunWorkerAsync(registerRequest);
        }
    }
}