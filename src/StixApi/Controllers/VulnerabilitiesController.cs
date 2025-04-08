using MediatR;
using Microsoft.AspNetCore.Mvc;
using StixApi.Features.Vulnerabilities.Commands;
using StixApi.Features.Vulnerabilities.Commands.Create;
using StixApi.Features.Vulnerabilities.Queries.List;
using StixApi.Features.Vulnerabilities.Queries.Get;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<ActionResult<IEnumerable<VulnerabilityListDTO>>> Get()
    {
        _logger.LogInformation("Retrieving all vulnerabilities.");

        var vulnerabilities = await _mediator.Send(new GetVulnerabilitiesQuery());
        return Ok(vulnerabilities);
    }

    /// <summary>
    /// This endpoint retrieves a specific vulnerability by its ID.
    /// </summary>
    /// <param name="id">The ID of the vulnerability to retrieve</param>
    /// <returns>The details of the specified vulnerability</returns>
    /// <remarks>
    /// A Vulnerability is a mistake in software that can be directly used by a hacker to gain access to a system or network.
    /// </remarks>
    /// <response code="200">Returns the details of the specified vulnerability</response>
    /// <response code="404">If the vulnerability is not found</response>
    /// <response code="500">If there is an internal server error</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<VulnerabilityDetailDTO>> Get(string id)
    {
        _logger.LogInformation($"Retrieving vulnerability with ID: {id}.");

        var vulnerability = await _mediator.Send(new GetVulnerabilityQuery { Id = id });
        return Ok(vulnerability);
    }

    /// <summary>
    /// This endpoint should create a new vulnerability using the STIX II vulnerability model.
    /// </summary>
    /// <remarks>
    /// A Vulnerability is a mistake in software that can be directly used by a hacker to gain access to a system or network.
    /// </remarks>`
    /// <param name="createVulnerabilityCommand"></param>
    /// <returns>The id of the newly created vulnerability</returns>
    [HttpPost]
    public async Task<ActionResult<int>> Post([FromBody] CreateVulnerabilityCommand createVulnerabilityCommand)
    {
        _logger.LogInformation("Creating a new vulnerability.");
        var id = await _mediator.Send(createVulnerabilityCommand);

        return Ok(id);
    }

    // PUT: v1/api/vulnerabilities/{id} - This endpoint should update an existing vulnerability by its ID
    // using the STIX II vulnerability model.
    [Authorize]
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
