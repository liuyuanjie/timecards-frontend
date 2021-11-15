using System;

namespace Timecards.Infrastructure.Model
{
    public class AccountResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Role { get; set; }
        public Guid AccountId { get; set; }
    }
}