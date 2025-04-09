namespace StixApi.Domain.Common;

public record ExternalReference(
    string SourceName,
    string? ExternalId = null,
    string? Description = null,
    string? Url = null,
    Dictionary<string, string>? Hashes = null);