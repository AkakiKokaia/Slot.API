using MediatR;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Roles.GetRoles;

public sealed record GetRolesQuery(string? name) : PagedRequestModel, IRequest<PagedResponse<List<GetRolesQueryResponse>>>;
