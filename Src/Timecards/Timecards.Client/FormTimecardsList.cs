using System.Windows.Forms;
using Timecards.Application.Commands;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;

namespace Timecards.Client
{
    public partial class FormTimecardsList : Form
    {
        private IQueryTimecardsCommand _queryTimecardsCommand;
        private IApproveTimecardsCommand _approveTimecardsCommand;
        private IDeclineTimecardsCommand _declineTimecardsCommand;

        public FormTimecardsList(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();
            _queryTimecardsCommand = new BWQueryTimecardsCommand(apiRequestFactory);
            _approveTimecardsCommand = new ApproveTimecardsCommand(apiRequestFactory);
            _declineTimecardsCommand = new BWDeclineTimecardsCommand(apiRequestFactory);
        }
    }
}