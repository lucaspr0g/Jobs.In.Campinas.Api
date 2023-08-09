using Domain.Commands.Account.Create;
using Domain.Commands.Account.Login;
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
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            try
            {
                return Ok(await _mediator.Send(request));
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> Create(AccountCreateRequest request)
        {
            try
            {
                return CreatedAtAction(nameof(Create), await _mediator.Send(request));
            }
            catch (Exception)
            {
                return Unauthorized();
            }
        }
    }
}