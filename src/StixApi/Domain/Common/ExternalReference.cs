namespace StixApi.Domain.Common;

public class ExternalReference
{
    /// <summary>
    /// The source within which the external-reference is defined (system, registry, organization, etc.)
    /// </summary>
    public required string SourceName { get; set; }

    /// <summary>
    /// An identifier for the external reference content.
    /// </summary>
    public string? ExternalId { get; set; }

    /// <summary>
    /// A human readable description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// A URL reference to an external resource [RFC3986].
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Specifies a dictionary of hashes for the file.
    /// </summary>
    public Dictionary<string, string>? Hashes { get; set; }
}