using FluentValidation;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Features.Roles.UpdateRole;

namespace Slot.Application.Features.Roles.Update;
public class UpdateRoleCommandValidator : CustomAbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("Role id is required!");

        RuleFor(x => x.name)
            .NotEmpty()
            .WithMessage("Role name is required!");
    }
}
