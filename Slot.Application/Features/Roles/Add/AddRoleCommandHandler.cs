using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Domain.Aggregates.Users.Entity;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Roles.AddRole;

public class AddRoleCommandHandler : IRequestHandler<AddRoleCommand, Response<Guid>>
{
    private readonly RoleManager<Role> _roleManager;

    public AddRoleCommandHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Response<Guid>> Handle(AddRoleCommand request, CancellationToken cancellationToken)
    {
        var existingRole = await _roleManager.Roles.FirstOrDefaultAsync(x => x.NormalizedName == request.name.ToUpper());

        if (existingRole != default)
            throw new ApiException(ApiExceptionCodeTypes.Exists);

        var role = new Role(request.name);

        await _roleManager.CreateAsync(role);

        return new Response<Guid>(role.Id);
    }
}
