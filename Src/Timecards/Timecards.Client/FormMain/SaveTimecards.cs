using System;
using System.Linq;
using System.Windows.Forms;
using Timecards.Infrastructure.Model;

namespace Timecards.Client
{
    public partial class FormMain
    {
        private void SaveTimecards()
        {
            if (!_inputWorkTimes.Any())
            {
                MessageBox.Show("Nothing need to be save!", "Save", MessageBoxButtons.OK);
                return;
            }

            var buildTimecardses = BuildTimecardses();
            var saveTimecardsRequest = new SaveTimecardsRequest
            {
                Timecardses = buildTimecardses
            };

            _saveTimecardsCommand.SaveTimecardsAsync(saveTimecardsRequest,
                responseState => { SaveTimecardsCallback(responseState, buildTimecardses.First().TimecardsDate); });
        }

        private void SaveTimecardsCallback(ResponseState responseState, DateTime timecardsDate)
        {
            if (responseState.IsSuccess)
            {
                MessageBox.Show("Save Successfully!", "Save", MessageBoxButtons.OK);
                LoadTimecardsOfDay(timecardsDate);
            }
            else
            {
                MessageBox.Show("Save Failed!", "Save", MessageBoxButtons.OK);
            }
        }
    }
}