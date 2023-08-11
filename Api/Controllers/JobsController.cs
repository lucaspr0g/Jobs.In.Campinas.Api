using Domain.Commands.Job.Create;
using Domain.Commands.Job.Delete;
using Domain.Commands.Job.Update;
using Domain.Queries.GetJob;
using Domain.Queries.GetJobs;
using Domain.Queries.GetUserJobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Api.Controllers
{
	[ApiController]
    [Authorize("Bearer")]
    [Route("api/v1/[controller]")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet, AllowAnonymous]
        public async Task<IActionResult> GetJobs()
        {
            var query = new GetJobsQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}"), AllowAnonymous]
        public async Task<IActionResult> GetJob(string id)
        {
            var query = new GetJobQuery(id);

            var job = await _mediator.Send(query);

            if (job is null)
                return NotFound();

            return Ok(job);
        }

        [HttpGet("getUserJobs")]
        public async Task<IActionResult> GetUserJobs()
        {
            var query = new GetUserJobsQuery();

            var job = await _mediator.Send(query);

            if (job is null)
                return NotFound();

            return Ok(job);
        }

        [HttpPost]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> CreateJob(CreateJobRequest request)
        {
            try
            {
                await _mediator.Send(request);
                return CreatedAtAction(nameof(CreateJob), request);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPatch]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> UpdateJob(UpdateJobRequest request)
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

		[HttpDelete("{id}")]
		[ProducesResponseType((int)HttpStatusCode.NoContent)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest)]
		public async Task<IActionResult> DeleteJob(string id)
		{
			try
			{
                var request = new DeleteJobRequest(id);
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