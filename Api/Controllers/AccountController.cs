using Domain.Commands.Account;
using Domain.Commands.Account.Confirm;
using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
using Domain.Commands.Account.Refresh;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/v1/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;   
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (InvalidOperationException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception)
			{
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(TokenResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(AccountCreateRequest request)
        {
            try
            {
                return CreatedAtAction(nameof(Create), await _mediator.Send(request));
            }
			catch (ArgumentException)
			{
				//todo log exception
				return BadRequest("Erro ao fazer o cadastro, tente novamente.");
			}
			catch (Exception e)
			{
				return BadRequest(e.Message);
			}
        }

        [HttpPost]
		[Route("[action]")]
        public async Task<IActionResult> Refresh(RefreshRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception e)
            {
				return BadRequest(e.Message);
			}
		}

		[HttpPost]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		[Route("[action]")]
		public async Task<IActionResult> Confirm(ConfirmRequest request)
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