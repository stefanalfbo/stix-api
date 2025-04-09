using FluentValidation;
using System.Text.RegularExpressions;

namespace StixApi.Features.Vulnerabilities.Commands.Create.V1;

public class ExternalReferenceDTOValidator : AbstractValidator<ExternalReferenceDTO>
{
    private static readonly Regex CvePattern = new("^cve$", RegexOptions.IgnoreCase);
    private static readonly Regex CapecPattern = new("^capec$", RegexOptions.IgnoreCase);

    private static readonly Regex CveIdPattern = new("^CVE-\\d{4}-(0\\d{3}|[1-9]\\d{3,})$");
    private static readonly Regex CapecIdPattern = new("^CAPEC-\\d+$");

    public ExternalReferenceDTOValidator()
    {
        RuleFor(x => x.SourceName)
            .NotEmpty().WithMessage("source_name is required");

        // If source_name is 'cve'
        When(x => CvePattern.IsMatch(x.SourceName ?? ""), () =>
        {
            RuleFor(x => x.ExternalId)
                .NotEmpty().WithMessage("external_id is required when source_name is 'cve'")
                .Matches(CveIdPattern).WithMessage("external_id must match the CVE pattern (e.g. CVE-2025-0042)")
                .When(x => x.ExternalId != null);
        });

        // If source_name is 'capec'
        When(x => CapecPattern.IsMatch(x.SourceName ?? ""), () =>
        {
            RuleFor(x => x.ExternalId)
                .NotEmpty().WithMessage("external_id is required when source_name is 'capec'")
                .Matches(CapecIdPattern).WithMessage("external_id must match the CAPEC pattern (e.g. CAPEC-1234)")
                .When(x => x.ExternalId != null);
        });

        // If source_name is not 'cve' or 'capec'
        When(x => !CvePattern.IsMatch(x.SourceName ?? "") && !CapecPattern.IsMatch(x.SourceName ?? ""), () =>
        {
            RuleFor(x => x).Must(x =>
                                x.ExternalId != null
                                || x.Description != null
                                || x.Url != null)
                .WithMessage("Must supply at least one of: external_id, description, or url");

            RuleFor(x => x.ExternalId)
                .Matches(@"^((CVE-\\d{4}-(0\\d{3}|[1-9]\\d{3,}))|(CAPEC-\\d+))$").WithMessage("external_id must match the CVE or CAPEC pattern (e.g. CVE-2025-0042 or CAPEC-1234)")
                .When(x => x.ExternalId != null);
        });

        RuleForEach(x => x.Hashes)
            .ChildRules(dict =>
            {
                // TODO: Handle custom hash types

                dict.RuleFor(kv => kv.Key)
                    .Matches("^(MD5|SHA-1|SHA-256|SHA-512|SHA3-256|SHA3-512|SSDEEP|TLSH)$")
                    .WithMessage("Invalid hash key. Valid keys: MD5, SHA-1, SHA-256, SHA-512, SHA3-256, SHA3-512, SSDEEP, TLSH");

                dict.RuleFor(kv => kv.Value)
                    .NotEmpty()
                    .WithMessage("Hash value cannot be empty");

                dict.RuleFor(kv => kv.Value)
                    .Matches("^[a-fA-F0-9]{32}$")
                    .When(kv => kv.Key == "MD5")
                    .WithMessage("MD5 hash must be 32 hexadecimal characters");

                dict.RuleFor(kv => kv.Value)
                    .Matches("^[0-9a-fA-F]{40}$")
                    .When(kv => kv.Key == "SHA-1")
                    .WithMessage("SHA-1 hash must be 40 hexadecimal characters");

                dict.RuleFor(kv => kv.Value)
                    .Matches("^[0-9a-fA-F]{64}$")
                    .When(kv => kv.Key == "SHA-256" || kv.Key == "SHA3-256")
                    .WithMessage("{PropertyName} must be 64 hexadecimal characters");

                dict.RuleFor(kv => kv.Value)
                    .Matches("^[0-9a-fA-F]{128}$")
                    .When(kv => kv.Key == "SHA-512" || kv.Key == "SHA3-512")
                    .WithMessage("{PropertyName} must be 128 hexadecimal characters");

                dict.RuleFor(kv => kv.Value)
                    .Matches("^[a-zA-Z0-9/+:.]{1,128}$")
                    .When(kv => kv.Key == "SSDEEP")
                    .WithMessage("SSDEEP hash must be at least 16 characters long");

                dict.RuleFor(kv => kv.Value)
                    .Matches("^[a-zA-Z0-9]{70}$")
                    .When(kv => kv.Key == "TLSH")
                    .WithMessage("TLSH hash must be at least 16 characters long");
            })
            .When(x => x.Hashes != null);
    }
}
