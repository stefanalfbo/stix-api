using Microsoft.AspNetCore.Mvc;

namespace StixApi.Controllers;

[ApiController]
[Route("[controller]")]
public class VulnerabilitiesController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<VulnerabilitiesController> _logger;

    public VulnerabilitiesController(ILogger<VulnerabilitiesController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetVulnerability")]
    public IEnumerable<Vulnerability> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new Vulnerability
        {
            Name = "Name"
        })
        .ToArray();
    }
}
