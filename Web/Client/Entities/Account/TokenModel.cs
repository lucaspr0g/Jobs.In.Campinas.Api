namespace Web.Client.Entities.Account
{
    public sealed class TokenModel
    {
        public string? AccessToken { get; set; }

        public string? RefreshToken { get; set; }
	}
}