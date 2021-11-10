using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Timecards.Infrastructure.Model
{
    public class ResponseState
    {
        public ResponseState(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
        public HttpStatusCode StatusCode { get; set; }
        public ResponseStateMessage ResponseStateMessage { get; set; }

        public bool IsSuccess => StatusCode == HttpStatusCode.OK
                                 || StatusCode == HttpStatusCode.Created
                                 || StatusCode == HttpStatusCode.NoContent;
    }

    public class ResponseStateMessage
    {
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public List<KeyValuePair<string, string>> SubErrorMessage { get; set; }

        public string OutputResponseMessage()
        {
            StringBuilder message = new StringBuilder();

            message.Append(ErrorMessage);

            if (SubErrorMessage != null && SubErrorMessage.Count > 0)
            {
                SubErrorMessage.ForEach(s => message.Append(s.Value));
            }

            return message.ToString();
        }
    }
}