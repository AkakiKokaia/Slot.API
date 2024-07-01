namespace Project.Application.Features.Roles.GetRoleById;

public class GetRoleByIdQueryResponse
{
    public GetRoleByIdQueryResponse(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    public Guid Id { get; set; }

    public string Name { get; set; }
}
