using MediatR;
using Microsoft.AspNetCore.Identity;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Features.Roles.UpdateRole;

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
