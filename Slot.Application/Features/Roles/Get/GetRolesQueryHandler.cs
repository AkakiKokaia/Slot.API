using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slot.Domain.Aggregates.Users.Entity;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Roles.GetRoles;

public class GetRolesQueryHandler : IRequestHandler<GetRolesQuery, PagedResponse<List<GetRolesQueryResponse>>>
{
    private readonly RoleManager<Role> _roleManager;

    public GetRolesQueryHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<PagedResponse<List<GetRolesQueryResponse>>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
    {
        var allRoles = _roleManager.Roles
            .Where(x =>
            string.IsNullOrEmpty(request.name) || x.Name.Contains(request.name));

        var roles = await allRoles.ToListAsync();

        return new PagedResponse<List<GetRolesQueryResponse>>(
            roles.Select(x => new GetRolesQueryResponse(x.Id, x.Name)).ToList(),
            request.pageNumber,
            request.pageSize,
            await allRoles.CountAsync());
    }
}
