namespace StixApi.Domain.Common;

public class GranularMarking
{
    /// <summary>
    /// A list of selectors for content contained within the STIX object in which this property appears.
    /// </summary>
    public required List<string> Selectors { get; set; }

    /// <summary>
    /// Identifies the language of the text identified by this marking.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// The marking_ref property specifies the ID of the marking-definition object that describes 
    /// the marking.
    /// </summary>
    public required string MarkingRef { get; set; }
}