using Newtonsoft.Json;
using RestSharp;
using Timecards.Infrastructure.Model;

namespace Timecards.Services
{
    public class ServiceBase
    {
        protected ResponseBase<T> BuildAsyncResponseResult<T>(IRestResponse<T> response) where T : class
        {
            var registerResponse = new ResponseBase<T>
            {
                ResponseState = new ResponseState
                {
                    StatusCode = response.StatusCode,
                }
            };

            if (registerResponse.ResponseState.IsSuccess)
            {
                registerResponse.ResponseResult = response.Data;
            }
            else
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

            return registerResponse;
        }
    }
}
