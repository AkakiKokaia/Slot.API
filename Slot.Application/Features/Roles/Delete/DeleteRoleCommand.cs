using MediatR;

namespace Slot.Application.Features.Roles.DeleteRole;

public sealed record DeleteRoleCommand(Guid id) : IRequest;
