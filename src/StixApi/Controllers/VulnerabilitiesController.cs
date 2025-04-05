using MediatR;
using Microsoft.AspNetCore.Mvc;
using StixApi.Features.Vulnerabilities.Commands;
using StixApi.Features.Vulnerabilities.Queries;
using StixApi.Features.Vulnerabilities.Queries.Models;

namespace StixApi.Controllers;

[ApiController]
[Route("v1/api/vulnerabilities")]
[Produces("application/json")]
public class VulnerabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(IMediator mediator, ILogger<VulnerabilitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    /// <summary>
    /// This endpoint retrieves a list of all vulnerabilities in the system.
    /// </summary>
    /// <returns>A list of all vulnerabilities</returns>
    /// <remarks>
    /// A Vulnerability is a mistake in software that can be directly used by a hacker to gain access to a system or network.
    /// </remarks>
    /// <response code="200">Returns a list of vulnerabilities</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VulnerabilityDTO>>> Get()
    {
        _logger.LogInformation("Retrieving all vulnerabilities.");

        var vulnerabilities = await _mediator.Send(new GetVulnerabilitiesQuery());
        return Ok(vulnerabilities);
    }

    // GET: v1/api/vulnerabilities/{id} - This endpoint should retrieve a specific vulnerability by its ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<VulnerabilityDTO>> Get(string id)
    {
        _logger.LogInformation($"Retrieving vulnerability with ID: {id}.");

        var vulnerability = await _mediator.Send(new GetVulnerabilityQuery { Id = id });
        return Ok(vulnerability);
    }

    // POST: v1/api/vulnerabilities - This endpoint should create a new vulnerability using the STIX II
    // vulnerability model.
    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateVulnerabilityCommand createVulnerabilityCommand)
    {
        _logger.LogInformation("Creating a new vulnerability.");
        var id = await _mediator.Send(createVulnerabilityCommand);

        return Ok(id);
    }

    // PUT: v1/api/vulnerabilities/{id} - This endpoint should update an existing vulnerability by its ID
    // using the STIX II vulnerability model.
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(string id, [FromBody] UpdateVulnerabilityCommand updateVulnerabilityCommand)
    {
        _logger.LogInformation($"Updating vulnerability with ID: {id}.");

        await _mediator.Send(updateVulnerabilityCommand);

        return NoContent();
    }

    // DELETE: v1/api/vulnerabilities/{id} - This endpoint should delete an existing vulnerability by its
    // ID.
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        _logger.LogInformation($"Deleting vulnerability with ID: {id}.");

        await _mediator.Send(new DeleteVulnerabilityCommand { Id = id });

        return NoContent();
    }
}
