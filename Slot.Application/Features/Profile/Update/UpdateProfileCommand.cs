using MediatR;

namespace Slot.Application.Features.Profile.UpdateProfile;

public sealed record UpdateProfileCommand(string email, string firstName, string lastName, string phoneNumber) : IRequest;
