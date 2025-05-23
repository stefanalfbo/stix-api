using FluentValidation.Results;

namespace StixApi.Exceptions;

public class ValidationException : Exception
{
    public List<string> ValdationErrors { get; set; }

    public ValidationException(ValidationResult validationResult)
    {
        ValdationErrors = new List<string>();

        foreach (var validationError in validationResult.Errors)
        {
            ValdationErrors.Add(validationError.ErrorMessage);
        }
    }
}