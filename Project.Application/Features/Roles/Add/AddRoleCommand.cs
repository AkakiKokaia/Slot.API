using MediatR;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Roles.AddRole;

public sealed record AddRoleCommand(string name) : IRequest<Response<Guid>>;
