namespace StixApi.Domain.Common;

public class GranularMarking
{
    public int Id { get; set; }
    public required ICollection<string> Selectors { get; set; }
    public string? Language { get; set; }
    public required string MarkingRef { get; set; }
}