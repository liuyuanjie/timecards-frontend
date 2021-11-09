namespace Timecards.Infrastructure.Model
{
    public class RegisterRequest
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public RoleType RoleType { get; set; }
    }

    public enum RoleType
    {
        Staff,
        Admin
    }
}
