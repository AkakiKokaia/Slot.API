﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Application.Features.Profile.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand>
{
    private readonly HttpContext _context;
    private readonly UserManager<User> _userManager;
    public ChangePasswordCommandHandler(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
    {
        _context = contextAccessor.HttpContext;
        _userManager = userManager;
    }

    public async Task Handle(ChangePasswordCommand command, CancellationToken cancellationToken)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var profile = await _userManager.FindByIdAsync(userId);

        if (profile == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        await _userManager.ChangePasswordAsync(profile, command.currentPassword, command.password);
    }
}
