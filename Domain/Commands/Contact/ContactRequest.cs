using MediatR;

namespace Domain.Commands.Contact
{
	public sealed class ContactRequest : IRequest
	{
        public string? Name { get; set; }

        public string? Email { get; set; }

        public string? Subject { get; set; }

		public string? Message { get; set; }

		public bool IsValid()
		{
			return !string.IsNullOrWhiteSpace(Name) && 
				!string.IsNullOrWhiteSpace(Email) && 
				!string.IsNullOrWhiteSpace(Subject) &&
				!string.IsNullOrWhiteSpace(Message);
		}
	}
}