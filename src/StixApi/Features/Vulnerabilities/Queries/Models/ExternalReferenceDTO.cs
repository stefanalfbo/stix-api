using System.Text.Json.Serialization;

namespace StixApi.Features.Vulnerabilities.Queries.Models;

/// <summary>
/// External references are used to describe pointers to 
/// information represented outside of STIX.
/// </summary>
public class ExternalReferenceDTO
{
    [JsonPropertyName("source_name")]
    public string SourceName { get; set; } = string.Empty;

    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    /// <summary>
    /// A human readable description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// A URL reference to an external resource.
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Specifies a dictionary of hashes for the file.
    /// </summary>
    [JsonPropertyName("hashes")]
    public Dictionary<string, string>? Hashes { get; set; }

    public bool IsCve => SourceName.ToLower() == "cve" &&
        ExternalId is not null &&
        System.Text.RegularExpressions.Regex.IsMatch(ExternalId, @"^CVE-\d{4}-(0\d{3}|[1-9]\d{3,})$");

    public bool IsCapec => SourceName.ToLower() == "capec" &&
        ExternalId is not null &&
        System.Text.RegularExpressions.Regex.IsMatch(ExternalId, @"^CAPEC-\d+$");

    public bool IsValid =>
        (!string.IsNullOrEmpty(SourceName)) &&
        (
            (!string.IsNullOrEmpty(ExternalId)) ||
            (!string.IsNullOrEmpty(Description)) ||
            (!string.IsNullOrEmpty(Url))
        );
}