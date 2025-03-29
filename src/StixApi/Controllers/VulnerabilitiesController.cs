using Microsoft.AspNetCore.Mvc;

namespace StixApi.Controllers;

[ApiController]
[Route("v1/api/[controller]")]
public class VulnerabilitiesController : ControllerBase
{
    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(ILogger<VulnerabilitiesController> logger)
    {
        _logger = logger;
    }

    // GET: v1/api/vulnerabilities - This endpoint retrieves a list of all vulnerabilities in the
    // system.
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Vulnerability>>> Get()
    {
        _logger.LogInformation("Retrieving all vulnerabilities.");
        return Ok(Enumerable.Range(1, 5).Select(index => new Vulnerability
        {
            Name = "Name"
        }));
    }

    // GET: v1/api/vulnerabilities/{id} - This endpoint should retrieve a specific vulnerability by its ID.
    [HttpGet("{id}")]
    public async Task<ActionResult<Vulnerability>> Get(int id)
    {
        _logger.LogInformation($"Retrieving vulnerability with ID: {id}.");
        return Ok(new Vulnerability
        {
            Name = "Name"
        });
    }

    // POST: v1/api/vulnerabilities - This endpoint should create a new vulnerability using the STIX II
    // vulnerability model.
    public async Task<ActionResult<int>> Post([FromBody] Vulnerability vulnerability)
    {
        _logger.LogInformation("Creating a new vulnerability.");
        return Ok(1);
    }

    // PUT: v1/api/vulnerabilities/{id} - This endpoint should update an existing vulnerability by its ID
    // using the STIX II vulnerability model.
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(int id, [FromBody] Vulnerability vulnerability)
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
