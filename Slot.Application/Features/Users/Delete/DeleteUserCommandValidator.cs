using FluentValidation;
using Slot.Application.Configuration.Exceptions;

namespace Slot.Application.Features.Users.Delete;
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
