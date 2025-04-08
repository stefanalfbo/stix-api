using AutoMapper;
using StixApi.Domain.Entities;
using StixApi.Persistance.Models;
using System.Text.Json;
using StixApi.Features.Vulnerabilities.Commands.Create.V1;
using StixApi.Features.Vulnerabilities.Commands.Update.V1;
using StixApi.Features.Vulnerabilities.Queries.List.V1;
using StixApi.Features.Vulnerabilities.Queries.Get.V1;

namespace StixApi.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<VulnerabilityDbModel, VulnerabilityDetailDTO>()
            .ConvertUsing<VulnerabilityDbModelToVulnerabilityDetailDTOConverter>();
        CreateMap<VulnerabilityDbModel, VulnerabilityListDTO>()
            .ConvertUsing<VulnerabilityDbModelToVulnerabilityListDTOConverter>();
        CreateMap<CreateVulnerabilityCommand, VulnerabilityDbModel>()
            .ConvertUsing<CreateVulnerabilityCommandToVulnerabilityDbModelConverter>();
        CreateMap<Vulnerability, VulnerabilityDbModel>()
            .ConvertUsing<VulnerabilityToVulnerabilityDbModelConverter>();
        CreateMap<VulnerabilityDbModel, Vulnerability>()
            .ConvertUsing<VulnerabilityDbModelToVulnerabilityConverter>();
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

public class VulnerabilityToVulnerabilityDbModelConverter : ITypeConverter<Vulnerability, VulnerabilityDbModel>
{
    public VulnerabilityDbModel Convert(Vulnerability source, VulnerabilityDbModel destination, ResolutionContext context)
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

public class VulnerabilityDbModelToVulnerabilityConverter : ITypeConverter<VulnerabilityDbModel, Vulnerability>
{
    public Vulnerability Convert(VulnerabilityDbModel source, Vulnerability destination, ResolutionContext context)
    {
        var json = source.Value?.RootElement.GetRawText() ?? throw new JsonException("Source Value is null and cannot be converted to JSON string.");
        var vulnerability = JsonSerializer.Deserialize<Vulnerability>(json);

        return vulnerability ?? throw new JsonException("Failed to deserialize Vulnerability from JSON.");
    }
}

public class UpdateVulnerabilityCommandToVulnerabilityDbModelConverter : ITypeConverter<UpdateVulnerabilityCommand, VulnerabilityDbModel>
{
    public VulnerabilityDbModel Convert(UpdateVulnerabilityCommand source, VulnerabilityDbModel destination, ResolutionContext context)
    {
        var json = destination.Value.RootElement.GetRawText();
        var dbValue = JsonSerializer.Deserialize<CreateVulnerabilityCommand>(json) ?? throw new JsonException("Failed to deserialize CreatVulnerabilityCommand from JSON.");

        dbValue.Description = source.Description;
        dbValue.Confidence = source.Confidence;
        dbValue.Revoked = source.Revoked;

        var dbModelAsJson = JsonSerializer.Serialize(dbValue);
        destination.Value = JsonDocument.Parse(dbModelAsJson);

        return destination;
    }
}
