using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Entity;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Profile.GetProfile;

public class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, Response<GetProfileQueryResponse>>
{
    private readonly HttpContext _context;
    private readonly UserManager<User> _userManager;
    public GetProfileQueryHandler(IHttpContextAccessor contextAccessor, UserManager<User> userManager)
    {
        _context = contextAccessor.HttpContext;
        _userManager = userManager;
    }

    public async Task<Response<GetProfileQueryResponse>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var profile = await _userManager.FindByIdAsync(userId);

        if (profile == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        return new Response<GetProfileQueryResponse>(new GetProfileQueryResponse(profile));
    }
}