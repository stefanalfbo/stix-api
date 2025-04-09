namespace StixApi.Domain.Common;

public record GranularMarking(
    List<string> Selectors,
    string MarkingRef,
    string? Language = null);