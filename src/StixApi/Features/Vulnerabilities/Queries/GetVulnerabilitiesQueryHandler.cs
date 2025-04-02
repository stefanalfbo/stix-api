using AutoMapper;
using MediatR;
using StixApi.Contracts.Persistance;
using StixApi.Features.Vulnerabilities.Queries.Models;
using StixApi.Persistance.Models;


namespace StixApi.Features.Vulnerabilities.Queries;

public class GetVulnerabilitiesQueryHandler : IRequestHandler<GetVulnerabilitiesQuery, List<VulnerabilityDTO>>
{
    private readonly IAsyncRepository<VulnerabilityDbModel> _vulnerabilityRepository;
    private readonly IMapper _mapper;

    public GetVulnerabilitiesQueryHandler(IMapper mapper, IAsyncRepository<VulnerabilityDbModel> vulnerabilityRepository)
    {
        _vulnerabilityRepository = vulnerabilityRepository;
        _mapper = mapper;
    }

    public async Task<List<VulnerabilityDTO>> Handle(GetVulnerabilitiesQuery request, CancellationToken cancellationToken)
    {
        var vulnerabilities = await _vulnerabilityRepository.ListAllAsync();

        return _mapper.Map<List<VulnerabilityDTO>>(vulnerabilities);
    }
}