using FluentValidation;
using Project.Application.Configuration.Exceptions;
using Project.Application.Features.Roles.AddRole;

namespace Project.Application.Features.Roles.Add;
public class AddRoleCommandValidator : CustomAbstractValidator<AddRoleCommand>
{
    public AddRoleCommandValidator()
    {
        RuleFor(x => x.name)
            .NotEmpty()
            .WithMessage("Role name is required!");
    }
}
