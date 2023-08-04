namespace Domain.Commands.Account.Login
{
    public sealed class LoginResponse
    {
        public string Token { get; set; } = string.Empty;

        public User User { get; set; } = new User(string.Empty, string.Empty, string.Empty);
    }

    public sealed record User(string UserId, string Email, string Name);
}