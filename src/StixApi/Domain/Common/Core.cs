namespace StixApi.Domain.Common;


public class Core
{
    public required string SpecificationVersion { get; set; }
    public required string Id { get; set; }
    public string? CreatedByReference { get; set; }
    public List<string>? Labels { get; set; }
    public required DateTime Created { get; set; }
    public required DateTime Modified { get; set; }
    public bool? Revoked { get; set; }
    public int? Confidence { get; set; }
    public string? Language { get; set; }
    public List<ExternalReference>? ExternalReferences { get; set; }
    public List<string>? ObjectMarkingRefs { get; set; }
    public List<GranularMarking>? GranularMarkings { get; set; }

    public Dictionary<string, object>? Extensions { get; set; }
}
