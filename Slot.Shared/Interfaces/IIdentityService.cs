namespace Slot.Shared.Interfaces;
public interface IIdentityService
{
    Guid? GetAuthorizedId();
    string GetAuthorizedEmail();
}
