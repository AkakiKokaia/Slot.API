using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Application.Configuration.Exceptions;
using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Common.Handlers;

public abstract class CustomCommandHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly HttpContext _context;
    protected string UserId => _context.User.Claims.First(c => c.Type == "UserID").Value;

    protected CustomCommandHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _context = contextAccessor.HttpContext;
    }

    protected async Task<User> GetUserAsync()
    {
        var user = await _userManager.FindByIdAsync(UserId);

        if (user == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        return user;
    }

    public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
}
