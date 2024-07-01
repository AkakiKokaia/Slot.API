using MediatR;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Profile.GetProfile;

public sealed record GetProfileQuery : IRequest<Response<GetProfileQueryResponse>>;
