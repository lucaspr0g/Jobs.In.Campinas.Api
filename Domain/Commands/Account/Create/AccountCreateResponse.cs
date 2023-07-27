namespace Domain.Commands.Account.Create
{
    public sealed class AccountCreateResponse
    {
        public AccountCreateResponse(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

        public bool IsSuccess { get; }

        public string Message { get; } = string.Empty;
    }
}