namespace Web.Client.Entities.Account
{
    public sealed class LoginResponse
    {
        public string? Token { get; set; }

        public UserModel? User { get; set; }
    }
}
