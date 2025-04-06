using MediatR;

namespace StixApi.Features.Vulnerabilities.Queries.List;

public class GetVulnerabilitiesQuery : IRequest<List<VulnerabilityListDTO>>
{
}