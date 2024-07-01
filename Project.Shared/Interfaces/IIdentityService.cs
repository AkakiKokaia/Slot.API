namespace Project.Shared.Interfaces;
public interface IIdentityService
{
    Guid? GetAuthorizedId();
    string GetAuthorizedEmail();
}
