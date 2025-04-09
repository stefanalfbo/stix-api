using MediatR;
using StixApi.Contracts.Persistance;
using StixApi.Domain.Entities;
using StixApi.Persistance.Models;
using System.Text.Json;


namespace StixApi.Features.Vulnerabilities.Queries.List.V1;

public class ListVulnerabilitiesQueryHandler : IRequestHandler<ListVulnerabilitiesQuery, List<VulnerabilityListDTO>>
{
    private readonly IAsyncRepository<VulnerabilityDbModel> _vulnerabilityRepository;


    public ListVulnerabilitiesQueryHandler(IAsyncRepository<VulnerabilityDbModel> vulnerabilityRepository)
    {
        _vulnerabilityRepository = vulnerabilityRepository;
    }

    public async Task<List<VulnerabilityListDTO>> Handle(ListVulnerabilitiesQuery request, CancellationToken cancellationToken)
    {
        var vulnerabilities = await _vulnerabilityRepository.ListAllAsync();

        var vulnerabilityList = new List<VulnerabilityListDTO>();
        foreach (var v in vulnerabilities)
        {
            var vulnerability = v.Value.Deserialize<Vulnerability>() ?? throw new JsonException("Failed to deserialize Vulnerability from JSON.");

            var vulnerabilityListDTO = new VulnerabilityListDTO
            {
                Id = vulnerability.Id,
                Name = vulnerability.Name,
                Created = vulnerability.Created.ToString("o"),
                Description = vulnerability.Description,
                Confidence = vulnerability.Confidence,
            };

            vulnerabilityList.Add(vulnerabilityListDTO);
        }

        return vulnerabilityList;
    }
}