using MediatR;

namespace Project.Application.Features.Users.Delete;
public sealed record DeleteUserCommand(Guid userId) : IRequest;
