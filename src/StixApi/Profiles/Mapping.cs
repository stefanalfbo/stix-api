using AutoMapper;
using StixApi.Features.Vulnerabilities.Models;
using StixApi.Domain.Entities;
using StixApi.Features.Vulnerabilities.Commands;
using StixApi.Persistance.Models;
using System.Text.Json;
using StixApi.Features.Vulnerabilities.Commands.Create;
using StixApi.Features.Vulnerabilities.Queries.List;
using StixApi.Features.Vulnerabilities.Queries.Get;

namespace StixApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Vulnerability, VulnerabilityDTO>().ReverseMap();


        CreateMap<VulnerabilityDbModel, VulnerabilityDetailDTO>()
            .ConvertUsing<VulnerabilityDbModelToVulnerabilityDetailDTOConverter>();
        CreateMap<VulnerabilityDbModel, VulnerabilityListDTO>()
            .ConvertUsing<VulnerabilityDbModelToVulnerabilityListDTOConverter>();
        CreateMap<CreateVulnerabilityCommand, VulnerabilityDbModel>()
            .ConvertUsing<CreateVulnerabilityCommandToVulnerabilityDbModelConverter>();

        CreateMap<UpdateVulnerabilityCommand, VulnerabilityDbModel>()
            .ConvertUsing<UpdateVulnerabilityCommandToVulnerabilityDbModelConverter>();
    }
}

public class VulnerabilityDbModelToVulnerabilityDetailDTOConverter : ITypeConverter<VulnerabilityDbModel, VulnerabilityDetailDTO>
{
    public VulnerabilityDetailDTO Convert(VulnerabilityDbModel source, VulnerabilityDetailDTO destination, ResolutionContext context)
    {
        var dto = source.Value.Deserialize<VulnerabilityDetailDTO>() ?? throw new JsonException("Failed to deserialize VulnerabilityDetailDTO from JSON.");

        return dto;
    }
}

public class VulnerabilityDbModelToVulnerabilityListDTOConverter : ITypeConverter<VulnerabilityDbModel, VulnerabilityListDTO>
{
    public VulnerabilityListDTO Convert(VulnerabilityDbModel source, VulnerabilityListDTO destination, ResolutionContext context)
    {
        var dto = source.Value.Deserialize<VulnerabilityListDTO>() ?? throw new JsonException("Failed to deserialize VulnerabilityListDTO from JSON.");

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
