using System;

namespace Timecards.Infrastructure.Model
{
    public class RegisterResponse: ResponseBase
    {
        public RegisterResponseResult ResponseResult { get; set; }
    }

    public class RegisterResponseResult
    {
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public RoleType RoleType { get; set; }
    }
}
