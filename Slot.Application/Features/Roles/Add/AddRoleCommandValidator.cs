using FluentValidation;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Features.Roles.AddRole;

namespace Slot.Application.Features.Roles.Add;
public class AddRoleCommandValidator : CustomAbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(x => x.name)
            .NotEmpty()
            .WithMessage("Role name is required!");
    }
}
