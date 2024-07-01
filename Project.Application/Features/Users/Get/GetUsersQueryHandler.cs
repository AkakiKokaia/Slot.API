using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project.Application.Features.Users.DataModels;
using Project.Domain.Aggregates.Users.Entity;
using Project.Shared.Configuration.Wrappers;
using System.Data;

namespace Project.Application.Features.Users.Get;
public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, PagedResponse<GetUsersQueryResponse>>
{
    private readonly UserManager<User> _userManager;
    public GetUsersQueryHandler(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<PagedResponse<GetUsersQueryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var usersQuery = _userManager.Users
            .Where(x => x.IsDeleted != request.deletedUsers);

        var users = await usersQuery.Select(x =>
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
                x.LockoutEnd)).ToListAsync();

        return new PagedResponse<GetUsersQueryResponse>(
            new GetUsersQueryResponse(users),
            request.pageNumber,
            request.pageSize,
            await usersQuery.CountAsync());
    }
}
