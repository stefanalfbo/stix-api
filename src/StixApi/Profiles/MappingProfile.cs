using AutoMapper;
using StixApi.Features.Vulnerabilities.Queries.Models;
using StixApi.Domain.Entities;

namespace StixApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vulnerability, VulnerabilityDTO>().ReverseMap();
    }
}