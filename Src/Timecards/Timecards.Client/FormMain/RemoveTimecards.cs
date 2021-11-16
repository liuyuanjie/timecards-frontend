using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Timecards.Infrastructure.Model;
using TimecardsControl;
using TimecardsControl.Extensions;

namespace Timecards.Client
{
    public partial class FormMain
    {
        private void RemoveTimecards(InputWorkTime inputWorkTime, InputWorkTimeControl control)
        {
            if (!inputWorkTime.TimecardsId.HasValue)
            {
                RemoveInputWorkTime(inputWorkTime, control);
                return;
            }

            _deleteTimecardsCommand.DeleteTimecardsAsync(new DeleteTimecardsRequest()
                {
                    TimecardsId = inputWorkTime.TimecardsId.Value
                },
                responseState => { RemoveTimecardsCallback(inputWorkTime, control, responseState); });
        }

        private void RemoveTimecardsCallback(InputWorkTime inputWorkTime, InputWorkTimeControl control,
            ResponseState responseState)
        {
            if (responseState.IsSuccess)
            {
                RemoveInputWorkTime(inputWorkTime, control);
                UpdateTotalWorkHour(_inputWorkTimes.Select(x => x.SaveTimecards.Invoke())
                    .Sum(x => x.Items.Sum(t => t.Hour)));
            }
            else
            {
                MessageBox.Show("Delete Failed", "Delete", MessageBoxButtons.OK);
            }
        }
    }
}