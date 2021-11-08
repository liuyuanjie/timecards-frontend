namespace Timecards.Infrastructure
{
    public interface IRegisterCommand
    {
        void Login(RegisterRequest registerRequest, Action callbackProcess);
    }
}