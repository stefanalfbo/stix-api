using System.Text.Json.Serialization;

namespace StixApi.Features.Vulnerabilities.Models;

/// <summary>
/// Common properties and behavior across all STIX Domain Objects and STIX Relationship Objects.
/// </summary>
public class CoreDTO
{
    /// <summary>
    /// The type property identifies the type of STIX Object (SDO, Relationship Object, etc). The 
    /// value of the type field MUST be one of the types defined by a STIX Object (e.g., indicator).
    /// </summary>
    [JsonPropertyName("type")]
    public required string Type { get; set; }
    /// <summary>
    /// The version of the STIX specification used to represent this object.
    /// </summary>
    [JsonPropertyName("spec_version")]
    public required string SpecificationVersion { get; set; }

    /// <summary>
    /// The id property universally and uniquely identifies this object.
    /// </summary>
    [JsonPropertyName("id")]
    public required string Id { get; set; }

    /// <summary>
    /// The ID of the Source object that describes who created this object.
    /// </summary>
    [JsonPropertyName("created_by_ref")]
    public string? CreatedByReference { get; set; }

    /// <summary>
    /// The labels property specifies a set of terms used to describe this object.
    /// </summary>
    [JsonPropertyName("labels")]
    public List<string>? Labels { get; set; }

    /// <summary>
    /// The created property represents the time at which the first version of this object was 
    /// created. The timstamp value MUST be precise to the nearest millisecond.
    /// </summary>
    [JsonPropertyName("created")]
    public required DateTime Created { get; set; }

    /// <summary>
    /// The modified property represents the time that this particular version of the object was 
    /// modified. The timstamp value MUST be precise to the nearest millisecond.
    /// </summary>
    [JsonPropertyName("modified")]
    public required DateTime Modified { get; set; }

    /// <summary>
    /// The revoked property indicates whether the object has been revoked.
    /// </summary>
    [JsonPropertyName("revoked")]
    public bool? Revoked { get; set; }

    /// <summary>
    /// Identifies the confidence that the creator has in the correctness of their data.
    /// </summary>
    [JsonPropertyName("confidence")]
    public int? Confidence { get; set; }

    /// <summary>
    /// Identifies the language of the text content in this object.
    /// </summary>
    [JsonPropertyName("lang")]
    public string? Language { get; set; }

    /// <summary>
    /// A list of external references which refers to non-STIX information.
    /// </summary>
    [JsonPropertyName("external_references")]
    public List<ExternalReferenceDTO>? ExternalReferences { get; set; }

    /// <summary>
    /// The list of marking-definition objects to be applied to this object.
    /// </summary>
    [JsonPropertyName("object_marking_refs")]
    public List<string>? ObjectMarkingRefs { get; set; }

    /// <summary>
    /// The set of granular markings that apply to this object.
    /// </summary>
    [JsonPropertyName("granular_markings")]
    public List<GranularMarkingDTO>? GranularMarkings { get; set; }

    /// <summary>
    /// Specifies any extensions of the object, as a dictionary.
    /// </summary>
    [JsonPropertyName("extensions")]
    public Dictionary<string, object>? Extensions { get; set; }
}
