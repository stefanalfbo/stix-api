using AutoMapper;
using MediatR;
using StixApi.Contracts.Persistance;
using StixApi.Persistance.Models;


namespace StixApi.Features.Vulnerabilities.Queries.List.V1;

public class ListVulnerabilitiesQueryHandler : IRequestHandler<ListVulnerabilitiesQuery, List<VulnerabilityListDTO>>
{
    private readonly IAsyncRepository<VulnerabilityDbModel> _vulnerabilityRepository;
    private readonly IMapper _mapper;

    public ListVulnerabilitiesQueryHandler(IMapper mapper, IAsyncRepository<VulnerabilityDbModel> vulnerabilityRepository)
    {
        _vulnerabilityRepository = vulnerabilityRepository;
        _mapper = mapper;
    }

    public async Task<List<VulnerabilityListDTO>> Handle(ListVulnerabilitiesQuery request, CancellationToken cancellationToken)
    {
        var vulnerabilities = await _vulnerabilityRepository.ListAllAsync();

        return _mapper.Map<List<VulnerabilityListDTO>>(vulnerabilities);
    }
}