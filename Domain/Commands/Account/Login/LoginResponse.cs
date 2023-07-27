namespace Domain.Commands.Account.Login
{
    public sealed class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string UserId { get; set; } = string.Empty;

        public string Message { get; set; } = string.Empty;
    }
}