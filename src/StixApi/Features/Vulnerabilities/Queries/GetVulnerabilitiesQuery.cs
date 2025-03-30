using MediatR;
using StixApi.Features.Vulnerabilities.Queries.Models;

namespace StixApi.Features.Vulnerabilities.Queries;

public class GetVulnerabilitiesQuery : IRequest<List<VulnerabilityDTO>>
{
}