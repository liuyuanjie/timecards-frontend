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
            if (!_inputWorkTimeSource.HasInputWorkTime())
            {
                MessageBox.Show("Nothing need to be save!", "Save", MessageBoxButtons.OK);
                return;
            }

            var saveTimecardsRequest = new SaveTimecardsRequest
            {
                Timecardses = _inputWorkTimeSource.ConvertTo()
            };

            _saveTimecardsCommand.SaveTimecardsAsync(saveTimecardsRequest,
                responseState => { SaveTimecardsCallback(responseState, _inputWorkTimeSource.InputTimecardsDay()); });
        }

        private void SaveTimecardsCallback(ResponseState responseState, DateTime timecardsDate)
        {
            if (!responseState.IsSuccess)
            {
                MessageBox.Show("Save Failed!", "Save", MessageBoxButtons.OK);
                return;
            }

            LoadTimecardsOfDay(timecardsDate);
        }
    }
}