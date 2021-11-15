using System;
using System.Collections.Generic;

namespace Timecards.Infrastructure.Model
{
    public class DeleteTimecardsRequest
    {
        public Guid TimecardsId { get; set; }
    }
}