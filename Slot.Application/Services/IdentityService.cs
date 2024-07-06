using Microsoft.AspNetCore.Http;
using Slot.Shared.Interfaces;
using System.Security.Claims;

namespace Slot.Application.Services;
public class IdentityService : IIdentityService
{
    private IHttpContextAccessor _context;

    public IdentityService(IHttpContextAccessor context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Guid? GetAuthorizedId()
    {
        var UserId = _context.HttpContext?.User?.FindFirst("UserID")?.Value;
        if(string.IsNullOrEmpty(UserId)) return null;
        return Guid.Parse(UserId);
    }

    public string GetAuthorizedEmail()
    {
        return _context.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
    }
}
