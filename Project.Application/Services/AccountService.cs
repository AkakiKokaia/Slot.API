using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Users.Entity;
using Project.Domain.Aggregates.Users.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Application.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private IConfiguration _configuration;
    public AccountService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    public async Task<User> ValidateUserSignInAsync(string username, string password)
    {
        var user = await _userManager.FindByNameAsync(username);

        if (user == default)
            await _userManager.FindByEmailAsync(username);

        if (user == default)
            throw new ApiException(ApiExceptionCodeTypes.InvalidCredentials);

        var result = await _signInManager.PasswordSignInAsync(
            user.UserName,
            password,
            true,
            lockoutOnFailure: true);

        if (!result.Succeeded)
            throw new ApiException(ApiExceptionCodeTypes.InvalidCredentials);

        if (result.Succeeded && result.IsLockedOut)
            throw new ApiException(ApiExceptionCodeTypes.UserLockedOut);

        return user;
    }

    public async Task<string> CreateTokenAsync(User user)
    {
        var role = await _userManager.GetRolesAsync(user);

        IdentityOptions _options = new IdentityOptions();

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Issuer = _configuration["JwtSettings:Issuer"],
            Subject = new ClaimsIdentity(new Claim[]
            {
                        new Claim("UserID",user.Id.ToString()),
                        new Claim(_options.ClaimsIdentity.RoleClaimType,role.FirstOrDefault())
            }),
            Expires = DateTime.UtcNow.AddDays(1),
            SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                        SecurityAlgorithms.HmacSha256Signature
                )
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        var token = tokenHandler.WriteToken(securityToken);

        return token;
    }
}
