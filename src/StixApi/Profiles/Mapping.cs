using AutoMapper;
using StixApi.Features.Vulnerabilities.Models;
using StixApi.Domain.Entities;
using StixApi.Features.Vulnerabilities.Commands;
using StixApi.Persistance.Models;
using System.Text.Json;

namespace StixApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vulnerability, VulnerabilityDTO>().ReverseMap();
        CreateMap<VulnerabilityDbModel, VulnerabilityDTO>()
            .ConvertUsing<VulnerabilityDbModelToDtoConverter>();

        CreateMap<CreateVulnerabilityCommand, VulnerabilityDbModel>().ConvertUsing<CreateVulnerabilityCommandToVulnerabilityDbModelConverter>();
        CreateMap<UpdateVulnerabilityCommand, VulnerabilityDbModel>()
            .ConvertUsing<UpdateVulnerabilityCommandToVulnerabilityDbModelConverter>();
    }
}

public class VulnerabilityDbModelToDtoConverter : ITypeConverter<VulnerabilityDbModel, VulnerabilityDTO>
{
    public VulnerabilityDTO Convert(VulnerabilityDbModel source, VulnerabilityDTO destination, ResolutionContext context)
    {
        var dto = source.Value.Deserialize<VulnerabilityDTO>() ?? throw new JsonException("Failed to deserialize VulnerabilityDTO from JSON.");

        return dto;
    }
}

public class CreateVulnerabilityCommandToVulnerabilityDbModelConverter : ITypeConverter<CreateVulnerabilityCommand, VulnerabilityDbModel>
{
    public VulnerabilityDbModel Convert(CreateVulnerabilityCommand source, VulnerabilityDbModel destination, ResolutionContext context)
    {
        var json = JsonSerializer.Serialize(source);
        var jsonDocument = JsonDocument.Parse(json);

        return new VulnerabilityDbModel
        {
            Id = source.Id,
            Value = jsonDocument
        };
    }
}

public class UpdateVulnerabilityCommandToVulnerabilityDbModelConverter : ITypeConverter<UpdateVulnerabilityCommand, VulnerabilityDbModel>
{
    public VulnerabilityDbModel Convert(UpdateVulnerabilityCommand source, VulnerabilityDbModel destination, ResolutionContext context)
    {
        var json = JsonSerializer.Serialize(source);
        var jsonDocument = JsonDocument.Parse(json);

        return new VulnerabilityDbModel
        {
            Id = source.Id,
            Value = jsonDocument
        };
    }
}
