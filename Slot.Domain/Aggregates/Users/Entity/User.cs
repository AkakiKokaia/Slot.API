using Microsoft.AspNetCore.Identity;

namespace Slot.Domain.Aggregates.Users.Entity;

public class User : IdentityUser<Guid>
{
    public User() { }

    public User(string userName, string? phoneNumber, string? email, string? firstName, string? lastName)
    {
        Id = Guid.NewGuid();
        UserName = userName;
        PhoneNumber = phoneNumber;
        Email = email;
        FirstName = firstName;
        LastName = lastName;
        CreatedAt = DateTimeOffset.UtcNow;
    }

    public void UpdateUser(string? email, string? phoneNumber, string? firstName, string? lastName)
    {
        if (Email != email)
        {
            Email = email;
            EmailConfirmed = false;
        }

        if (PhoneNumber != phoneNumber)
        {
            PhoneNumber = phoneNumber;
            PhoneNumberConfirmed = false;
        }

        FirstName = firstName;
        LastName = lastName;
        SetUpdateDate();
    }

    public override Guid Id { get; set; }

    public override string? UserName { get; set; }

    public override string? PhoneNumber { get; set; }

    public override string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }
    public decimal Balance { get; set; }

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public DateTimeOffset? DeletedAt { get; private set; }

    public bool IsDeleted { get; private set; }

    public DateTimeOffset? LastTokenSentTime { get; private set; }

    public void DeleteUser()
    {
        DeletedAt = DateTimeOffset.UtcNow;
        IsDeleted = true;
        SetUpdateDate();
    }

    public void SetUpdateDate()
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
