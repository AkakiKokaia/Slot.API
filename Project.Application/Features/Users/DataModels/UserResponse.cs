namespace Project.Application.Features.Users.DataModels;
public sealed record UserResponse(
    Guid Id,
    string userName,
    string? phoneNumber,
    string? email,
    string? firstName,
    string? lastName,
    bool emailConfirmed,
    bool phoneNumberConfirmed,
    DateTimeOffset createdAt,
    DateTimeOffset? updatedAt,
    DateTimeOffset? deletedAt,
    bool isDeleted,
    DateTimeOffset? lockoutEnd);
