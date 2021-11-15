using System.Collections.Generic;
using System.Text;

namespace Timecards.Infrastructure.Model
{
    public class ResponseBase<T>
    {
        public ResponseState ResponseState { get; set; }
        public T ResponseResult { get; set; }
    }
}