using MediatR;

namespace Project.Application.Features.Roles.DeleteRole;

public sealed record DeleteRoleCommand(Guid id) : IRequest;
