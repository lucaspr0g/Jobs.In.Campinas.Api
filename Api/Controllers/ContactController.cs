using Domain.Commands.Contact;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
	[ApiController]
	[Route("api/v1/[controller]")]
	public class ContactController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ContactController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("Send")]
		public async Task<IActionResult> Send(ContactRequest request)
		{
			try
			{
				await _mediator.Send(request);
				return NoContent();
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
		}
	}
}