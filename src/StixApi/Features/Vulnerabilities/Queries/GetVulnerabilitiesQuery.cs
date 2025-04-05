using MediatR;
using StixApi.Features.Vulnerabilities.Models;

namespace StixApi.Features.Vulnerabilities.Queries;

public class GetVulnerabilitiesQuery : IRequest<List<VulnerabilityDTO>>
{
}