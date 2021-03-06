using System.Windows.Forms;
using Timecards.Infrastructure.Model;
using TimecardsControl;

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
            if (!responseState.IsSuccess)
            {
                MessageBox.Show("Delete Failed", "Delete", MessageBoxButtons.OK);
                return;
            }

            RemoveInputWorkTime(inputWorkTime, control);
            UpdateTotalWorkHour(_inputWorkTimeSource.GetTotalHours());
        }
    }
}