using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Entity;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Roles.GetRoleById;

public class GetRoleByIdQueryHandler : IRequestHandler<GetRoleByIdQuery, Response<GetRoleByIdQueryResponse>>
{
    private readonly RoleManager<Role> _roleManager;
    public GetRoleByIdQueryHandler(RoleManager<Role> roleManager)
    {
        _roleManager = roleManager;
    }

    public async Task<Response<GetRoleByIdQueryResponse>> Handle(GetRoleByIdQuery request, CancellationToken cancellationToken)
    {
        var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == request.id);

        if (role == default)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        return new Response<GetRoleByIdQueryResponse>(new GetRoleByIdQueryResponse(role.Id, role.Name));
    }
}
