using MediatR;

namespace Slot.Application.Features.Roles.UpdateRole;

public sealed record UpdateRoleCommand(Guid id, string name) : IRequest;
