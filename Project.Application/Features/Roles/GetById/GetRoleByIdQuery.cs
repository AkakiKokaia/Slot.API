using MediatR;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Roles.GetRoleById;

public sealed record class GetRoleByIdQuery(Guid id) : IRequest<Response<GetRoleByIdQueryResponse>>;
