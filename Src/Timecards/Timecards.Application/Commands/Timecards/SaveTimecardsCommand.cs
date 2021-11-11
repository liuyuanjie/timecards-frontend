using System;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Services;
using Timecards.Services.Impl;

namespace Timecards.Application.Commands
{
    public class SaveTimecardsCommand : ISaveTimecardsCommand
    {
        private readonly ITimecardsServices _timecardsServices;

        public SaveTimecardsCommand(IApiRequestFactory apiRequestFactory)
        {
            _timecardsServices = new TimecardsSerivice(apiRequestFactory);
        }

        public void SaveTimecardsAsync(SaveTimecardsRequest saveTimecardsRequest,
            Action<ResponseState> callbackProcess)
        {
            _timecardsServices.SaveTimecardsAsync(saveTimecardsRequest, callbackProcess);
        }
    }
}