using Newtonsoft.Json;
using RestSharp;
using Timecards.Infrastructure.Model;

namespace Timecards.Services.Impl
{
    public class ServiceBase
    {
        protected ResponseBase<T> BuildAsyncResponseResult<T>(IRestResponse<T> response) where T : class
        {
            var serverResponse = new ResponseBase<T>
            {
                ResponseState = new ResponseState(response.StatusCode)
            };

            if (serverResponse.ResponseState.IsSuccess)
            {
                serverResponse.ResponseResult = response.Data;
                return serverResponse;
            }

            CollectFailedStateMessage(response, serverResponse);

            return serverResponse;
        }

        private static void CollectFailedStateMessage<T>(IRestResponse<T> restResponse, ResponseBase<T> response)
            where T : class
        {
            if (restResponse.ErrorException != null)
            {
                response.ResponseState.ResponseStateMessage = new ResponseStateMessage
                {
                    ErrorCode = "RequestFailed",
                    ErrorMessage = restResponse.ErrorMessage
                };
            }
            else
            {
                response.ResponseState.ResponseStateMessage =
                    JsonConvert.DeserializeObject<ResponseStateMessage>(restResponse.Content) ?? new ResponseStateMessage();
            }
        }

        protected ResponseState BuildResponseState(IRestResponse response)
        {
            var responseState = new ResponseState(response.StatusCode)
            {
                ResponseStateMessage = response.ErrorException != null
                    ? new ResponseStateMessage
                    {
                        ErrorCode = "RequestFailed",
                        ErrorMessage = response.ErrorMessage
                    }
                    : JsonConvert.DeserializeObject<ResponseStateMessage>(response.Content) ?? new ResponseStateMessage()
            };

            return responseState;
        }
    }
}