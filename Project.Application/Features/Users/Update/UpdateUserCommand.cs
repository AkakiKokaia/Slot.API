using MediatR;

namespace Project.Application.Features.Users.Update;
public sealed record UpdateUserCommand(Guid userId, string email, string firstName, string lastName, string phoneNumber) : IRequest;