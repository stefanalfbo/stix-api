using MediatR;
using Microsoft.AspNetCore.Mvc;
using StixApi.Features.Vulnerabilities.Queries;
using StixApi.Features.Vulnerabilities.Queries.Models;

namespace StixApi.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class VulnerabilitiesController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(IMediator mediator, ILogger<VulnerabilitiesController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    // GET: v1/api/vulnerabilities - This endpoint retrieves a list of all vulnerabilities in the
    // system.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<VulnerabilityDTO>>> Get()
    {
        _logger.LogInformation("Retrieving all vulnerabilities.");

        var vulnerabilities = await _mediator.Send(new GetVulnerabilitiesQuery());
        return Ok(vulnerabilities);
    }

    // GET: v1/api/vulnerabilities/{id} - This endpoint should retrieve a specific vulnerability by its ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<VulnerabilityDTO>> Get(int id)
    {
        _logger.LogInformation($"Retrieving vulnerability with ID: {id}.");
        return Ok(new VulnerabilityDTO
        {
            SpecificationVersion = "2.1",
            Id = "vulnerability--12345678-1234-5678-1234-567812345678",
            Name = "Vulnerability Name",
            Description = "Vulnerability Description",
            Created = DateTime.UtcNow,
            Modified = DateTime.UtcNow,
        });

    }

    // POST: v1/api/vulnerabilities - This endpoint should create a new vulnerability using the STIX II
    // vulnerability model.
    public async Task<ActionResult<int>> Post([FromBody] VulnerabilityDTO vulnerability)
    {
        _logger.LogInformation("Creating a new vulnerability.");
        return Ok(1);
    }

    // PUT: v1/api/vulnerabilities/{id} - This endpoint should update an existing vulnerability by its ID
    // using the STIX II vulnerability model.
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] VulnerabilityDTO vulnerability)
    {
        _logger.LogInformation($"Updating vulnerability with ID: {id}.");
        return NoContent();
    }

    // DELETE: v1/api/vulnerabilities/{id} - This endpoint should delete an existing vulnerability by its
    // ID.
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        _logger.LogInformation($"Deleting vulnerability with ID: {id}.");
        return NoContent();
    }
}
