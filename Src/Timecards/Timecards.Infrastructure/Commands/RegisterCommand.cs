namespace Timecards.Infrastructure
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IAccountService _accountService;
        public LoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _accountService = new AccountService(_apiRequestFactory);
        }

        public void Login(RegisterRequest registerRequest, Action callbackProcess)
        {
            _accountService.AsyncLogin(registerRequest, () => CallbackProcess());
        }
    }
}