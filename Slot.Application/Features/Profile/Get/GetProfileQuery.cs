using MediatR;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Profile.GetProfile;

public sealed record GetProfileQuery : IRequest<Response<GetProfileQueryResponse>>;
