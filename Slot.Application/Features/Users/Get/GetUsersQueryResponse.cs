using Slot.Application.Features.Users.DataModels;

namespace Slot.Application.Features.Users.Get;
public class GetUsersQueryResponse
{
    public GetUsersQueryResponse(ICollection<UserResponse> users)
    {
        Users = users.ToList();
    }
    public List<UserResponse> Users { get; private set; }
}