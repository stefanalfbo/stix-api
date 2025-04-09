using FluentValidation;

namespace StixApi.Features.Vulnerabilities.Commands.Create.V1;

public class GranularMarkingDTOValidator : AbstractValidator<GranularMarkingDTO>
{
    public GranularMarkingDTOValidator()
    {
        RuleFor(v => v.Selectors)
            .NotEmpty()
            .WithMessage("selectors must contain at least one selector");

        RuleForEach(v => v.Selectors)
            .Matches(@"^([a-z0-9_-]{3,249}(\\.(\\[\\d+\\]|[a-z0-9_-]{1,250}))*|id)$")
            .WithMessage(@"Each selector item must follow the regex: ^([a-z0-9_-]{3,249}(\\.(\\[\\d+\\]|[a-z0-9_-]{1,250}))*|id)$");

        RuleFor(v => v.MarkingRef)
            .Matches(@"^marking-definition--[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[1-5][0-9a-fA-F]{3}-[89abAB][0-9a-fA-F]{3}-[0-9a-fA-F]{12}$")
            .WithMessage("marking_ref must follow the pattern: marking-definition--GUID");
    }
}
