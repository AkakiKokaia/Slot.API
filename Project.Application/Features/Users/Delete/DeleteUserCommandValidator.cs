using FluentValidation;
using Project.Application.Configuration.Exceptions;

namespace Project.Application.Features.Users.Delete;
public class DeleteUserCommandValidator : CustomAbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(x => x.userId)
            .NotNull()
            .NotEmpty()
            .WithMessage("UserId is required!");
    }
}
