﻿using Domain.Commands.Job;
using Domain.Queries.GetJob;
using Domain.Queries.GetJobs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetJobs()
        {
            var query = new GetJobsQuery();
            return Ok(await _mediator.Send(query));
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetJob(string id)
        {
            var query = new GetJobQuery(id);

            var job = await _mediator.Send(query);

            if (job is null)
                return NotFound();

            return Ok(job);
        }

        [HttpPost]
        public async Task<IActionResult> CreateJob(CreateJobRequest request)
        {
            try
            {
                await _mediator.Send(request);
                return CreatedAtAction(nameof(CreateJob), request);
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}