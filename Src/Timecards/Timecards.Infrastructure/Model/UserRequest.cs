using System;

namespace Timecards.Infrastructure.Model
{
    public class UserRequest
    {
        public Guid AccountId { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
}