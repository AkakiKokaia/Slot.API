using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Slot.Application.Features.Users.DataModels;
using Slot.Domain.Aggregates.Users.Entity;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Users.GetById;
public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Response<UserResponse>>
{
    private readonly UserManager<User> _userManager;
    public GetUserByIdQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<Response<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users
            .Where(x => x.Id == request.userId)
            .Select(x =>
            new UserResponse(
                x.Id,
                x.UserName,
                x.PhoneNumber,
                x.Email,
                x.FirstName,
                x.LastName,
                x.EmailConfirmed,
                x.PhoneNumberConfirmed,
                x.CreatedAt,
                x.UpdatedAt,
                x.DeletedAt,
                x.IsDeleted,
                x.LockoutEnd))
            .FirstOrDefaultAsync();

        return new Response<UserResponse>(user);
    }
}
