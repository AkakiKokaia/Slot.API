using MediatR;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Roles.GetRoles;

public sealed record GetRolesQuery(string? name) : PagedRequestModel, IRequest<PagedResponse<List<GetRolesQueryResponse>>>;
