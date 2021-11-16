using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Timecards.Application.Commands;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

namespace Timecards.Client.FormTimecardsList
{
    public partial class FormTimecardsList : Form
    {
        private readonly IQueryTimecardsCommand _queryTimecardsCommand;
        private readonly IApproveTimecardsCommand _approveTimecardsCommand;
        private readonly IDeclineTimecardsCommand _declineTimecardsCommand;

        public FormTimecardsList(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();

            _queryTimecardsCommand = new BWQueryTimecardsCommand(apiRequestFactory);
            _approveTimecardsCommand = new ApproveTimecardsCommand(apiRequestFactory);
            _declineTimecardsCommand = new BWDeclineTimecardsCommand(apiRequestFactory);

            InitialTimecardsDataGridView();
            LoadData();
        }

        private void LoadData()
        {
            _queryTimecardsCommand.GetAsync(new QueryTimecardsRequest(),
                responseResult => LoadCallback(responseResult));
        }

        private void LoadCallback(ResponseBase<List<TimecardsResult>> responseResult)
        {
            if (!responseResult.ResponseState.IsSuccess)
            {
                MessageBox.Show(
                    responseResult.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Load",
                    MessageBoxButtons.OK);
                return;
            }

            dataGridViewTimecards.DataSource = TimecardsBindingModel.ConvertTo(responseResult.ResponseResult);
        }

        private void buttonApprove_Click(object sender, EventArgs e)
        {
            _approveTimecardsCommand.ApproveTimecardsAsync(new BatchTimecardsRequest()
                {
                    TimecardsIds = GetSelectedTimecardsIds()
                },
                responseResult => ExecuteCallback(responseResult));
        }

        private void buttonDecline_Click(object sender, EventArgs e)
        {
            _declineTimecardsCommand.DeclineTimecardsAsync(new BatchTimecardsRequest()
                {
                    TimecardsIds = GetSelectedTimecardsIds()
                },
                responseResult => ExecuteCallback(responseResult));
        }

        private void ExecuteCallback(ResponseState responseResult)
        {
            if (!responseResult.IsSuccess)
            {
                MessageBox.Show(
                    responseResult.ResponseStateMessage.OutputResponseMessage(),
                    "Update",
                    MessageBoxButtons.OK);
                return;
            }

            LoadData();
        }
    }
}