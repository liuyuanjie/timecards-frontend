using System;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model
{
    public class BatchTimecardsRequest
    {
        public List<Guid> TimecardsIds { get; set; }
    }
}