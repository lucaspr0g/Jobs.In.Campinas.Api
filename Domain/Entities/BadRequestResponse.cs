namespace Domain.Entities
{
	public sealed class BadRequestResponse
	{
        public BadRequestResponse(string message)
        {
            Message = message;
        }

        public string? Message { get; set; }
    }
}