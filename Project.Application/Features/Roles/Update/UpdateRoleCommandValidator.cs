using FluentValidation;
using Project.Application.Configuration.Exceptions;
using Project.Application.Features.Roles.UpdateRole;

namespace Project.Application.Features.Roles.Update;
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
