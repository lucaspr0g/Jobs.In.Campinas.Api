using Domain.Entities;
using Domain.Interfaces.Repositories;
using Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/jobs")]
    public class JobsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetJobs()
        {
            var query = new GetJobsQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobModel model)
        {
            //await _jobRepository.CreateAsync(model);
            return CreatedAtAction(nameof(CreateJob), model);
        }
    }
}