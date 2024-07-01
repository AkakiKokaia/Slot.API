namespace Project.Application.Features.Roles.GetRoles;

public class GetRolesQueryResponse
{
    public GetRolesQueryResponse(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}
