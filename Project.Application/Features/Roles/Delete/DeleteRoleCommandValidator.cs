using FluentValidation;
using Project.Application.Configuration.Exceptions;
using Project.Application.Features.Roles.DeleteRole;

namespace Project.Application.Features.Roles.Delete;
public class DeleteRoleCommandValidator : CustomAbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(x => x.id)
            .NotEmpty()
            .WithMessage("Id is requred!");
    }
}
