using MediatR;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Account.SignIn;

public sealed record SignInCommand(string username, string password) : IRequest<Response<SignInCommandResponse>>;
