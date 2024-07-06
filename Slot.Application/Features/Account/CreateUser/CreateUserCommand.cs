using MediatR;

namespace Slot.Application.Features.Account.CreateUser;

public sealed record CreateUserCommand(string userName, string? phoneNumber, string? email, string? firstName, string? lastName, string password, string confirmPassword) : IRequest;