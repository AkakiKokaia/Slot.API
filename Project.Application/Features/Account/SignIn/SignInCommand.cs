using MediatR;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Account.SignIn;

public sealed record SignInCommand(string username, string password) : IRequest<Response<SignInCommandResponse>>;
