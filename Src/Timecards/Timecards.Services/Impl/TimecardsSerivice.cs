using System;
using System.Collections.Generic;
using RestSharp;
using Timecards.Application;
using Timecards.Infrastructure;
using Timecards.Infrastructure.Model;

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
            request.AddHeader("Authorization", $"Bearer {TokenStore.Login.Token}");
            request.AddQueryParameter(nameof(QueryTimecardsRequest.UserId), queryTimecardsRequest.UserId.ToString());
            request.AddQueryParameter(nameof(QueryTimecardsRequest.TimecardsDate),
                queryTimecardsRequest.TimecardsDate.ToString());

            _apiRequestFactory.CreateClient().ExecuteAsyncGet<List<TimecardsResult>>(request,
                (response, e) => { 
                    callbackProcessHandler.Invoke(BuildAsyncResponseResult(response));
                },
                Method.GET.ToString());
        }

        public void SaveTimecardsAsync(SaveTimecardsRequest saveTimecardsRequest,
            Action<ResponseState> callbackProcessHandler)
        {
            var request = new RestRequest(IdentityTokenEndPoint, Method.POST);
            request.AddHeader("Authorization", $"Bearer {TokenStore.Login.Token}");
            request.AddJsonBody(saveTimecardsRequest);

            _apiRequestFactory.CreateClient().ExecuteAsyncPost(request,
                (response, e) => { callbackProcessHandler.Invoke(BuildResponseState(response)); },
                Method.POST.ToString());
        }
    }
}