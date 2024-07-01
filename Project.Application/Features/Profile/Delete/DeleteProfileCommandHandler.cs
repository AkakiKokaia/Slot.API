﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Application.Features.Profile.DeleteProfile;

public class DeleteProfileCommandHandler : IRequestHandler<DeleteProfileCommand>
{
    private readonly HttpContext _context;
    private readonly UserManager<User> _userManager;
    public DeleteProfileCommandHandler(IHttpContextAccessor httpContextAccessor,
                                       UserManager<User> userManager)
    {
        _context = httpContextAccessor.HttpContext;
        _userManager = userManager;
    }

    public async Task Handle(DeleteProfileCommand request, CancellationToken token)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        user.DeleteUser();

        await _userManager.UpdateAsync(user);
    }
}