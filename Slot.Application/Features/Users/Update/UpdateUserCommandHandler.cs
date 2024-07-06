using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Features.Users.Update;
public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand>
{
    private readonly UserManager<User> _userManager;
    public UpdateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.userId);

        if (user == default) throw new ApiException(ApiExceptionCodeTypes.NotFound);

        user.UpdateUser(request.email, request.phoneNumber, request.firstName, request.lastName);

        await _userManager.UpdateAsync(user);
    }
}
