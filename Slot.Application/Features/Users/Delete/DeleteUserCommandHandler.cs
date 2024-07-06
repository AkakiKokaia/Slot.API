﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Features.Users.Delete;
public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
{
    private readonly HttpContext _context;
    private readonly UserManager<User> _userManager;
    public DeleteUserCommandHandler(IHttpContextAccessor httpContextAccessor,
                                    UserManager<User> userManager)
    {
        _context = httpContextAccessor.HttpContext;
        _userManager = userManager;
    }

    public async Task Handle(DeleteUserCommand request, CancellationToken token)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        user.DeleteUser();

        await _userManager.UpdateAsync(user);
    }
}