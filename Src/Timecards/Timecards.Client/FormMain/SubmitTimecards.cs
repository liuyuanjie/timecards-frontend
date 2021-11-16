using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Timecards.Application;
using Timecards.Infrastructure.Model;

namespace Timecards.Client
{
    public partial class FormMain
    {
        private void SubmitTimecards()
        {
            var dialogResult =
                MessageBox.Show("You won't update the records if you decide to submit. Do you still want to submit?",
                    "Submit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            var timecardses = BuildTimecardses();

            _submitTimecardsCommand.SubmitTimecardsAsync(
                new BatchTimecardsRequest
                {
                    TimecardsIds = timecardses.Where(x => x.TimecardsId.HasValue).Select(x => x.TimecardsId.Value)
                        .ToList()
                },
                responseState => { SubmitTimecardsCallback(responseState, timecardses.First().TimecardsDate); });
        }

        private List<Infrastructure.Model.Timecards> BuildTimecardses()
        {
            var timecardses = _inputWorkTimes.Select(x =>
                new Infrastructure.Model.Timecards(x.TimecardsId, AccountStore.Account.AccountId, x.ProjectId,
                    x.TimecardsDate.ConvertToUTCDate())
                {
                    TimecardsDate = x.TimecardsDate.ConvertToUTCDate(),
                    Items = x.SaveTimecards.Invoke().Items
                        .Select(t => new ItemcardsItem(t.WorkDay.ConvertToUTCDate(), t.Hour, t.Note)).ToList()
                }).ToList();

            return timecardses;
        }

        private void SubmitTimecardsCallback(ResponseState responseState, DateTime timecardsDate)
        {
            if (responseState.IsSuccess)
            {
                MessageBox.Show("Submit Successfully!", "Submit", MessageBoxButtons.OK);
                LoadTimecardsOfDay(timecardsDate);
            }
            else
            {
                MessageBox.Show("Submit Failed!", "Submit", MessageBoxButtons.OK);
            }
        }
    }
}