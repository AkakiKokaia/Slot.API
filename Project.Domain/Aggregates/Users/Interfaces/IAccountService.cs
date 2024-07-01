using Project.Domain.Aggregates.Users.Entity;

namespace Project.Domain.Aggregates.Users.Interfaces;
public interface IAccountService
{
    Task<User> ValidateUserSignInAsync(string username, string password);

    Task<string> CreateTokenAsync(User user);
}
