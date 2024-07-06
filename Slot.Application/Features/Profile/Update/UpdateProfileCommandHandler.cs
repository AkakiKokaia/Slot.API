using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Features.Profile.UpdateProfile;

public class UpdateProfileCommandHandler : IRequestHandler<UpdateProfileCommand>
{
    private readonly HttpContext _context;
    private readonly UserManager<User> _userManager;
    public UpdateProfileCommandHandler(IHttpContextAccessor httpContextAccessor,
                                       UserManager<User> userManager)
    {
        _context = httpContextAccessor.HttpContext;
        _userManager = userManager;
    }

    public async Task Handle(UpdateProfileCommand command, CancellationToken cancellationToken)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var profile = await _userManager.FindByIdAsync(userId);

        if (profile == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        profile.UpdateUser(command.email, command.phoneNumber, command.firstName, command.lastName);

        await _userManager.UpdateAsync(profile);
    }
}
