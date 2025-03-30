namespace StixApi.Domain.Common;

public class ExternalReference
{
    public int Id { get; set; }
    public string SourceName { get; set; } = string.Empty;
    public string? ExternalId { get; set; }
    public string? Description { get; set; }
    public string? Url { get; set; }
    public Dictionary<string, string>? Hashes { get; set; }
}