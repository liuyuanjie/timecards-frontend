namespace Timecards.Infrastructure.Model
{
    public class RegisterResponse: ResponseBase
    {
        public string FullName { get; set; }
        public string Email { get; set; }
    }
}
