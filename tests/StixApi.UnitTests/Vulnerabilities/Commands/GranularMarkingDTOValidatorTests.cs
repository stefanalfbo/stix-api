using Shouldly;
using StixApi.Features.Vulnerabilities.Commands.Create.V1;

namespace StixApi.UnitTests.Vulnerabilities.Commands;

public class GranularMarkingDTOTests
{
    [Fact]
    public async Task Valid_GranularMarking()
    {
        // Arrange
        var validator = new GranularMarkingDTOValidator();
        var granularMarking = new GranularMarkingDTO()
        {
            Selectors = new List<string> { "id" },
            MarkingRef = "marking-definition--0e1ea3ef-4046-48f2-8356-77ab0fe4847f",
        };

        // Act
        var validationResult = await validator.ValidateAsync(granularMarking);

        // Assert
        validationResult.IsValid.ShouldBeTrue();
    }

    [Fact]
    public async Task Selectors_Should_Not_Be_Empty()
    {
        // Arrange
        var validator = new GranularMarkingDTOValidator();
        var granularMarking = new GranularMarkingDTO()
        {
            Selectors = new List<string>(),
            MarkingRef = "marking-definition--0e1ea3ef-4046-48f2-8356-77ab0fe4847f",
        };

        // Act
        var validationResult = await validator.ValidateAsync(granularMarking);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
    }

    [Fact]
    public async Task MarkingRef_Needs_Start_With_The_Correct_Prefix()
    {
        // Arrange
        var validator = new GranularMarkingDTOValidator();
        var granularMarking = new GranularMarkingDTO()
        {
            Selectors = new List<string> { "id" },
            MarkingRef = "not-correct-prefix--0e1ea3ef-4046-48f2-8356-77ab0fe4847f",
        };

        // Act
        var validationResult = await validator.ValidateAsync(granularMarking);

        // Assert
        validationResult.IsValid.ShouldBeFalse();
    }
}