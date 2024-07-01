using MediatR;

namespace Project.Application.Features.Profile.UpdateProfile;

public sealed record UpdateProfileCommand(string email, string firstName, string lastName, string phoneNumber) : IRequest;
