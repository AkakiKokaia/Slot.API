using MediatR;

namespace Slot.Application.Features.Users.Delete;
public sealed record DeleteUserCommand(Guid userId) : IRequest;
