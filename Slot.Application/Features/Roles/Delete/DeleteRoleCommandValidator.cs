using FluentValidation;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Features.Roles.DeleteRole;

namespace Slot.Application.Features.Roles.Delete;
public class DeleteRoleCommandValidator : CustomAbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("Id is requred!");
    }
}
