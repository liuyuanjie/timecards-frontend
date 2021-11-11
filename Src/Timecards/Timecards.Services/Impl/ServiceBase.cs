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

        private static void CollectFailedStateMessage<T>(IRestResponse<T> response, ResponseBase<T> registerResponse)
            where T : class
        {
            if (response.ErrorException != null)
            {
                registerResponse.ResponseState.ResponseStateMessage = new ResponseStateMessage
                {
                    ErrorCode = "RequestFailed",
                    ErrorMessage = response.ErrorMessage
                };
            }
            else
            {
                registerResponse.ResponseState.ResponseStateMessage =
                    JsonConvert.DeserializeObject<ResponseStateMessage>(response.Content);
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
                    : JsonConvert.DeserializeObject<ResponseStateMessage>(response.Content)
            };

            return responseState;
        }
    }
}