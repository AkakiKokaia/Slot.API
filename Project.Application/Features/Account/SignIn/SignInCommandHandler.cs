﻿using MediatR;
using Project.Domain.Aggregates.Users.Interfaces;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Account.SignIn;

public class SignInCommandHandler : IRequestHandler<SignInCommand, Response<SignInCommandResponse>>
{
    private readonly IAccountService _accountService;

    public SignInCommandHandler(
        IAccountService accountService)
    {
        _accountService = accountService;
    }

    public async Task<Response<SignInCommandResponse>> Handle(SignInCommand request, CancellationToken cancellationToken)
    {
        var user = await _accountService.ValidateUserSignInAsync(request.username, request.password);

        var token = await _accountService.CreateTokenAsync(user);

        return new Response<SignInCommandResponse>(new SignInCommandResponse(token));
    }
}
