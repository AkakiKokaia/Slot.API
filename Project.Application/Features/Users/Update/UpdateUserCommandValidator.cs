using FluentValidation;
using Project.Application.Configuration.Exceptions;

namespace Project.Application.Features.Users.Update;
public class UpdateUserCommandValidator : CustomAbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(x => x.userId)
            .NotEmpty()
            .WithMessage("UserId is required!");

        RuleFor(x => x.firstName)
            .NotEmpty()
            .WithMessage("FirstName is required!");

        RuleFor(x => x.lastName)
            .NotEmpty()
            .WithMessage("LastName is required!");

        RuleFor(x => x.email)
            .NotEmpty()
            .WithMessage("email is required!");
    }
}
