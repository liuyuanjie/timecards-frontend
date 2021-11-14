using System;

namespace Timecards.Infrastructure.Model
{
    public class LoginResult
    {
        public string Token { get; set; }
        public Guid AccountId { get; set; }
    }
}   