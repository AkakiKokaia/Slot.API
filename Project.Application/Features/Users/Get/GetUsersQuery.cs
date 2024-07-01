using MediatR;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Users.Get;
public sealed record GetUsersQuery(bool? deletedUsers) : PagedRequestModel, IRequest<PagedResponse<GetUsersQueryResponse>>;
