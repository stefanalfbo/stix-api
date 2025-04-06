using AutoMapper;
using StixApi.Features.Vulnerabilities.Models;
using StixApi.Profiles;
using Shouldly;

namespace StixApi.UnitTests.Vulnerabilities.Models;

public class ExternalReferenceDTOTests
{
    private readonly IMapper _mapper;

    private readonly ExternalReferenceDTO externalReference = new()
    {
        SourceName = "cve",
        ExternalId = "CVE-2008-7026",
        Description = "Test Description",
        Url = "https://example.com",
        Hashes = new Dictionary<string, string>
        {
            { "SHA-256", "abcdef1234567890" }
        }
    };

    public ExternalReferenceDTOTests()
    {
        var configurationProvider = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });

        _mapper = configurationProvider.CreateMapper();
    }


    [Fact]
    public async Task Valid_External_Reference()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "cve",
            ExternalId = "CVE-2008-7026",
            Description = "Test Description",
            Url = "https://example.com",
            Hashes = new Dictionary<string, string>
            {
                { "MD5", "DEADBEEFDEADBEEFDEADBEEFDEADBEEF" }
            }
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task SourceName_Should_Not_Be_Empty()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "",
            ExternalId = "something",
            Description = "Test Description",
            Url = "https://example.com",
            Hashes = new Dictionary<string, string>
            {
                { "MD5", "DEADBEEFDEADBEEFDEADBEEFDEADBEEF" }
            }
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
        validationResult.Errors.Count.ShouldBe(2);
        validationResult.Errors[0].PropertyName.ShouldBe("SourceName");
        validationResult.Errors[0].ErrorMessage.ShouldBe("source_name is required");
    }

    [Fact]
    public async Task ExternalId_Needs_To_Start_With_CVE_If_SourceName_Is_cve()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "cve",
            ExternalId = "sve-2008-7026",
            Description = "Test Description",
            Url = "https://example.com",
            Hashes = new Dictionary<string, string>
            {
                { "MD5", "DEADBEEFDEADBEEFDEADBEEFDEADBEEF" }
            }
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
        validationResult.Errors.Count.ShouldBe(1);
        validationResult.Errors[0].PropertyName.ShouldBe("ExternalId");
        validationResult.Errors[0].ErrorMessage.ShouldBe("external_id must match the CVE pattern (e.g. CVE-2025-0042)");
    }

    [Fact]
    public async Task ExternalId_Needs_To_Start_With_CAPEC_If_SourceName_Is_capec()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "capec",
            ExternalId = "kapek-1337",
            Description = "Test Description",
            Url = "https://example.com",
            Hashes = new Dictionary<string, string>
            {
                { "MD5", "DEADBEEFDEADBEEFDEADBEEFDEADBEEF" }
            }
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
        validationResult.Errors.Count.ShouldBe(1);
        validationResult.Errors[0].PropertyName.ShouldBe("ExternalId");
        validationResult.Errors[0].ErrorMessage.ShouldBe("external_id must match the CAPEC pattern (e.g. CAPEC-1234)");
    }

    [Fact]
    public async Task SourceName_And_Url_Is_Enough()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "capec",
            Url = "https://example.com",
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task SourceName_And_Description_Is_Enough()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "capec",
            Description = "The description",
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task Only_SourceName_Is_Not_Enough()
    {
        // Arrange
        var validator = new ExternalReferenceDTOValidator();
        var externalReference = new ExternalReferenceDTO()
        {
            SourceName = "capec",
        };

        // Act
        var validationResult = await validator.ValidateAsync(externalReference);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
        validationResult.Errors.Count.ShouldBe(1);
        validationResult.Errors[0].ErrorMessage.ShouldBe("Must supply at least one of: external_id, description, or url");
    }
}