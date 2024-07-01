using Project.Application.Features.Users.DataModels;

namespace Project.Application.Features.Users.Get;
public class GetUsersQueryResponse
{
    public GetUsersQueryResponse(ICollection<UserResponse> users)
    {
        Users = users.ToList();
    }
    public List<UserResponse> Users { get; private set; }
}