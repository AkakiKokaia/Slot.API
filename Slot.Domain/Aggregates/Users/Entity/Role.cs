using Microsoft.AspNetCore.Identity;

namespace Slot.Domain.Aggregates.Users.Entity;

public class Role : IdentityRole<Guid>
{
    public Role() { }

    public Role(string roleName)
    {
        this.Name = roleName;
    }

    public void Update(string roleName)
    {
        this.Name = roleName;
    }
}
