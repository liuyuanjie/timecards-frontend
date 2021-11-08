namespace Timecards.Infrastructure
{
    public class LoginCommand : ILoginCommand
    {
        private readonly IdentityService _identityService;
        public LoginCommand(IApiRequestFactory apiRequestFactory)
        {
            _identityService = new IdentityService(_apiRequestFactory);
        }

        public Login(LoginRequest loginRequest, Action<LoginResponse> callbackProcess)
        {
            _identityService.AsyncLogin(loginRequest, (loginResponse) => CallbackProcess(loginResponse));
        }
    }
}