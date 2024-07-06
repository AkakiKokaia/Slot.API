using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Features.Profile.GetProfile;

public class GetProfileQueryResponse
{
    public GetProfileQueryResponse(User user)
    {
        UserName = user.UserName;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Email = user.Email;
        PhoneNumber = user.PhoneNumber;
        CreatedAt = user.CreatedAt;
    }

    public string? UserName { get; init; }
    public string? FirstName { get; init; }
    public string? LastName { get; init; }
    public string? Email { get; init; }
    public string? PhoneNumber { get; init; }
    public DateTimeOffset CreatedAt { get; init; }
}
