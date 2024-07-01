using MediatR;

namespace Project.Application.Features.Roles.UpdateRole;

public sealed record UpdateRoleCommand(Guid id, string name) : IRequest;
