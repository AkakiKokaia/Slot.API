using MediatR;
using Microsoft.AspNetCore.Identity;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Application.Features.Roles.UpdateRole;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand>
{
    private readonly RoleManager<Role> _roleManager;

    public UpdateRoleCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.FindByIdAsync(request.id.ToString());

        if (role == default)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        role.Update(request.name);

        await _roleManager.UpdateAsync(role);
    }
}
