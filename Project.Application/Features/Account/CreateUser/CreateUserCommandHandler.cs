using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Constants;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Application.Features.Account.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand>
{
    private readonly UserManager<User> _userManager;

    public CreateUserCommandHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await _userManager.Users.Where(x =>
                                                x.NormalizedUserName == request.userName.ToUpper() ||
                                                x.NormalizedEmail == request.email.ToUpper()).AnyAsync();

        if (existingUser) throw new ApiException(ApiExceptionCodeTypes.Exists);

        var user = new User(request.userName, request.phoneNumber, request.email, request.firstName, request.lastName);

        var userResult = await _userManager.CreateAsync(user, request.password);

        if (!userResult.Succeeded)
            throw new ApiException(ApiExceptionCodeTypes.Unhandled);

        var roleResult = await _userManager.AddToRoleAsync(user, RoleConstants.UserRoleString);
    }
}
