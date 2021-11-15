using System;
using System.Collections.Generic;
using RestSharp;
using Timecards.Application;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;
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
    }
}