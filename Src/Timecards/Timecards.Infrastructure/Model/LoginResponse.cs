namespace Timecards.Infrastructure.Model
{
    public class LoginResponse: ResponseBase
    {
        public LoginResponseResult ResponseResult { get; set; }
    }

    public class LoginResponseResult
    {
        public string Token { get; set; }
    }

}   