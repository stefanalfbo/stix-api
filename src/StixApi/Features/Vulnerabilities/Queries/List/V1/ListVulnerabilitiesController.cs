using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace StixApi.Features.Vulnerabilities.Queries.List.V1;

[ApiController]
[Route("v1/api/vulnerabilities")]
[Produces("application/json")]
public class ListVulnerabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<ListVulnerabilitiesController> _logger;

    public ListVulnerabilitiesController(IMediator mediator, ILogger<ListVulnerabilitiesController> logger)
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
    [SwaggerOperation(Tags = new[] { "vulnerabilities" })]
    public async Task<ActionResult<IEnumerable<VulnerabilityListDTO>>> List()
    {
        _logger.LogInformation("Retrieving all vulnerabilities.");

        var vulnerabilities = await _mediator.Send(new ListVulnerabilitiesQuery());
        return Ok(vulnerabilities);
    }
}
