using System.Text.Json.Serialization;

namespace StixApi.Features.Vulnerabilities.Commands.Create.V1;

/// <summary>
/// The granular-marking type defines how the list of marking-definition objects referenced by 
/// the marking_refs property to apply to a set of content identified by the list of selectors 
/// in the selectors property.
/// </summary>
public class GranularMarkingDTO
{
    /// <summary>
    /// A list of selectors for content contained within the STIX object in which this property appears.
    /// </summary>
    [JsonPropertyName("selectors")]
    public required List<string> Selectors { get; set; }

    /// <summary>
    /// Identifies the language of the text identified by this marking.
    /// </summary>
    [JsonPropertyName("lang")]
    public string? Language { get; set; }

    /// <summary>
    /// The marking_ref property specifies the ID of the marking-definition object that describes 
    /// the marking.
    /// </summary>
    [JsonPropertyName("marking_ref")]
    public required string MarkingRef { get; set; }
}