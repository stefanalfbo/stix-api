using System.Text.Json.Serialization;

namespace StixApi.Features.Vulnerabilities.Models;

/// <summary>
/// External references are used to describe pointers to 
/// information represented outside of STIX.
/// </summary>
public class ExternalReferenceDTO
{
    /// <summary>
    /// The source within which the external-reference is defined (system, registry, organization, etc.)
    /// </summary>
    [JsonPropertyName("source_name")]
    public required string SourceName { get; set; }

    /// <summary>
    /// An identifier for the external reference content.
    /// </summary>
    [JsonPropertyName("external_id")]
    public string? ExternalId { get; set; }

    /// <summary>
    /// A human readable description
    /// </summary>
    [JsonPropertyName("description")]
    public string? Description { get; set; }

    /// <summary>
    /// A URL reference to an external resource [RFC3986].
    /// </summary>
    [JsonPropertyName("url")]
    public string? Url { get; set; }

    /// <summary>
    /// Specifies a dictionary of hashes for the file.
    /// </summary>
    [JsonPropertyName("hashes")]
    public Dictionary<string, string>? Hashes { get; set; }
}