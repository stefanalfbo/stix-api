using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StixApi.Features.Vulnerabilities.Queries.Get.V1;

[ApiController]
[Route("v1/api/vulnerabilities")]
[Produces("application/json")]
public class GetVulnerabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<GetVulnerabilitiesController> _logger;

    public GetVulnerabilitiesController(IMediator mediator, ILogger<GetVulnerabilitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
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
}
