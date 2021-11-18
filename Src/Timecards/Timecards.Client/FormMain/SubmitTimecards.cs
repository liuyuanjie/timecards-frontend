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
                MessageBox.Show(
                    $"You won't update the records if you submit records.{Environment.NewLine}Are you sure want to submit?",
                    "Submit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.No)
            {
                return;
            }

            _submitTimecardsCommand.SubmitTimecardsAsync(new BatchTimecardsRequest
                {
                    TimecardsIds = _inputWorkTimeSource.GetCanSubmitTimecards().Select(x => x.TimecardsId.Value)
                        .ToList()
                },
                responseState => { SubmitTimecardsCallback(responseState, _inputWorkTimeSource.InputTimecardsDay()); });
        }

        private void SubmitTimecardsCallback(ResponseState responseState, DateTime timecardsDate)
        {
            if (!responseState.IsSuccess)
            {
                MessageBox.Show("Submit Failed!", "Submit", MessageBoxButtons.OK);
                return;
            }

            LoadTimecardsOfDay(timecardsDate);
        }
    }
}