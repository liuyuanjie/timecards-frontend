using System;

namespace Timecards.Infrastructure.Model
{
    public class QueryTimecardsRequest
    {
        public Guid? UserId { get; set; }
        public DateTime? TimecardsDate { get; set; }
    }
}