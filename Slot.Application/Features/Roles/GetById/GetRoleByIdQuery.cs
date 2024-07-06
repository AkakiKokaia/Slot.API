using MediatR;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Roles.GetRoleById;

public sealed record class GetRoleByIdQuery(Guid id) : IRequest<Response<GetRoleByIdQueryResponse>>;
