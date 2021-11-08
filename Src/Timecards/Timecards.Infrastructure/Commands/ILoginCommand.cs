namespace Timecards.Infrastructure
{
    public interface ILoginCommand
    {
        void Login(LoginRequest loginRequest, Action<LoginResponse> callbackProcess);
    }
}