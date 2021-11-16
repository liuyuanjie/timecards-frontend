using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Timecards.Application.Commands;
using Timecards.Application.Commands.Timecards;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Infrastructure.Model.Response;

namespace Timecards.Client.FormTimecardsList
{
    public partial class FormTimecardsList : Form
    {
        private readonly ISearchTimecardsCommand _searchTimecardsCommand;
        private readonly IApproveTimecardsCommand _approveTimecardsCommand;
        private readonly IDeclineTimecardsCommand _declineTimecardsCommand;

        public FormTimecardsList(IApiRequestFactory apiRequestFactory)
        {
            InitializeComponent();

            _searchTimecardsCommand = new BWSearchTimecardsCommand(apiRequestFactory);
            _approveTimecardsCommand = new ApproveTimecardsCommand(apiRequestFactory);
            _declineTimecardsCommand = new BWDeclineTimecardsCommand(apiRequestFactory);

            InitialTimecardsDataGridView();
            LoadData();
        }

        private void LoadData()
        {
            _searchTimecardsCommand.SearchAsync(new QueryTimecardsRequest(),
                responseResult => LoadCallback(responseResult));
        }

        private void LoadCallback(ResponseBase<List<SearchTimecardsResult>> responseResult)
        {
            if (!responseResult.ResponseState.IsSuccess)
            {
                MessageBox.Show(
                    responseResult.ResponseState.ResponseStateMessage.OutputResponseMessage(),
                    "Load",
                    MessageBoxButtons.OK);
                return;
            }

            dataGridViewTimecards.DataSource = responseResult.ResponseResult
                .Where(x => x.StatusType == StatusType.Submitted)
                .ToList();
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