using MediatR;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Roles.AddRole;

public sealed record AddRoleCommand(string name) : IRequest<Response<Guid>>;
