using FluentValidation.Results;

namespace Slot.Application.Configuration.Exceptions;

public class ValidationException : Exception
{
    public ValidationException() : base("Validation failure have occurred.")
    {
    }

    public List<string> ValidationErrors { get; set; } = new();

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        foreach (var failure in failures.GroupBy(x => x.PropertyName))
        {
            ValidationErrors.Add(failure.Key);
        }

    }

    public ValidationException(List<string>? failures)
        : this()
    {
        if (failures != null)
        {
            ValidationErrors = failures;
        }
    }
}