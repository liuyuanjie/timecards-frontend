using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Timecards.Application;
using Timecards.Infrastructure.Model;
using TimecardsControl;
using TimecardsControl.Extensions;

namespace Timecards.Client
{
    public partial class FormMain
    {
        private void LoadTimecardsOfDay(DateTime day)
        {
            var queryTimecardsRequest = new QueryTimecardsRequest()
            {
                UserId = AccountStore.Account.AccountId,
                TimecardsDate = day.ConvertToUTCDate()
            };

            _queryTimecardsCommand.GetAsync(queryTimecardsRequest, responseResult => LoadCallback(responseResult));
        }

        private void LoadCallback(ResponseBase<List<TimecardsResult>> responseResult)
        {
            if (responseResult.ResponseState.IsSuccess)
            {
                ClearInputWorkTimes();
                PopulateWorkTimes(responseResult);
                UpdateSubmitButtonState();
                UpdateSaveButtonState();
                UpdateNewButtonState();
            }
            else
            {
                MessageBox.Show(
                    responseResult.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Load",
                    MessageBoxButtons.OK);
            }
        }

        private void PopulateWorkTimes(ResponseBase<List<TimecardsResult>> responseResult)
        {
            responseResult.ResponseResult.ForEach(x =>
            {
                var inputWorkTime = new InputWorkTime(x.TimecardsId, x.UserId, x.ProjectId, x.StatusType)
                {
                    ProjectName = x.ProjectName,
                    TimecardsDate = x.TimecardsDate.Date
                };
                var dataSource = new TimecardsDataSource()
                {
                    Items = x.Items.Select(s => new Item(s.WorkDay, s.Hour, s.Note)).ToList()
                };
                AddInputWorkTime(inputWorkTime, dataSource);
            });

            UpdateTotalWorkHour(responseResult.ResponseResult.Sum(x => x.Items.Sum(t => t.Hour)));
        }

        private void ClearInputWorkTimes()
        {
            splitContainerWorkTime.Panel2.Controls.RemoveAllInputWorkTimes();
            _inputWorkTimes.Clear();
        }
    }
}