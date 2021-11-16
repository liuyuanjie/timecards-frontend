using System;
using System.Collections.Generic;
using RestSharp;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
using Timecards.Infrastructure.Model.Response;
using Timecards.Services.Extensions;

namespace Timecards.Services.Impl
{
    public class TimecardsSerivice : ServiceBase, ITimecardsServices
    {
        const string IdentityTokenEndPoint = "timecards";

        private readonly IApiRequestFactory _apiRequestFactory;

        public TimecardsSerivice(IApiRequestFactory apiRequestFactory)
        {
            _apiRequestFactory = apiRequestFactory;
        }

        public void GetTimecardsAsync(QueryTimecardsRequest queryTimecardsRequest,
            Action<ResponseBase<List<TimecardsResult>>> callbackProcessHandler)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddAuthorizationHeader();
            request.AddQueryParameter(nameof(QueryTimecardsRequest.UserId), queryTimecardsRequest.UserId.ToString());
            request.AddQueryParameter(nameof(QueryTimecardsRequest.TimecardsDate),
                queryTimecardsRequest.TimecardsDate.ToString());

            _apiRequestFactory.CreateClient().ExecuteAsyncGet<List<TimecardsResult>>(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildAsyncResponseResult(response)); },
                Method.GET.ToString());
        }

        public ResponseBase<List<TimecardsResult>> GetTimecards(QueryTimecardsRequest queryTimecardsRequest)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.GET);
            request.AddAuthorizationHeader();
            request.AddQueryParameter(nameof(QueryTimecardsRequest.UserId), queryTimecardsRequest.UserId.ToString());
            request.AddQueryParameter(nameof(QueryTimecardsRequest.TimecardsDate),
                queryTimecardsRequest.TimecardsDate.ToString());
            var response = _apiRequestFactory.CreateClient().Execute<List<TimecardsResult>>(request);

            return BuildAsyncResponseResult(response);
        }

        public ResponseBase<List<SearchTimecardsResult>> SearchTimecards(QueryTimecardsRequest queryTimecardsRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/search", Method.GET);
            request.AddAuthorizationHeader();
            var response = _apiRequestFactory.CreateClient().Execute<List<SearchTimecardsResult>>(request);

            return BuildAsyncResponseResult(response);
        }

        public void SaveTimecardsAsync(SaveTimecardsRequest saveTimecardsRequest,
            Action<ResponseState> callbackProcessHandler)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddAuthorizationHeader();
            request.AddJsonBody(saveTimecardsRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildResponseState(response)); },
                Method.POST.ToString());
        }

        public ResponseState SaveTimecards(SaveTimecardsRequest saveTimecardsRequest)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddAuthorizationHeader();
            request.AddJsonBody(saveTimecardsRequest.Timecardses);

            return BuildResponseState(_apiRequestFactory.CreateClient().Execute(request));
        }

        public ResponseState DeleteTimecards(DeleteTimecardsRequest deleteTimecardsRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/{deleteTimecardsRequest.TimecardsId}", Method.DELETE);
            request.AddAuthorizationHeader();
            
            return BuildResponseState(_apiRequestFactory.CreateClient().Execute(request));
        }

        public ResponseState SubmitTimecards(BatchTimecardsRequest submitTimecardsRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/submit", Method.POST);
            request.AddAuthorizationHeader();
            request.AddJsonBody(submitTimecardsRequest.TimecardsIds);
            
            return BuildResponseState(_apiRequestFactory.CreateClient().Execute(request));
        }

        public ResponseState Approveimecards(BatchTimecardsRequest approveTimecardsRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/approve", Method.POST);
            request.AddAuthorizationHeader();
            request.AddJsonBody(approveTimecardsRequest.TimecardsIds);

            return BuildResponseState(_apiRequestFactory.CreateClient().Execute(request));
        }

        public ResponseState DeclineTimecards(BatchTimecardsRequest declineTimecardsRequest)
        {
            var request = new RestRequest($"{IdentityTokenEndPoint}/decline", Method.POST);
            request.AddAuthorizationHeader();
            request.AddJsonBody(declineTimecardsRequest.TimecardsIds);

            return BuildResponseState(_apiRequestFactory.CreateClient().Execute(request));
        }
    }
}