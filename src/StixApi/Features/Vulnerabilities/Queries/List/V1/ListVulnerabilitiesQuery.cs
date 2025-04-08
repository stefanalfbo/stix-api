using MediatR;

namespace StixApi.Features.Vulnerabilities.Queries.List.V1;

public class ListVulnerabilitiesQuery : IRequest<List<VulnerabilityListDTO>>
{
}