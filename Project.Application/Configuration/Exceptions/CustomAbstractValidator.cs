using FluentValidation;
using FluentValidation.Results;

namespace Project.Application.Configuration.Exceptions;

public abstract class CustomAbstractValidator<T> : AbstractValidator<T>
{
    public override ValidationResult Validate(ValidationContext<T> context)
    {
        try
        {
            var result = base.Validate(context);
            if (!result.IsValid)
            {
                throw new Project.Application.Configuration.Exceptions.ValidationException(result.Errors);
            }
            return result;
        }
        catch (FluentValidation.ValidationException ex)
        {
            throw new Project.Application.Configuration.Exceptions.ValidationException(ex.Errors);
        }
    }
}