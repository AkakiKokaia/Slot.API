using MediatR;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Users.Get;
public sealed record GetUsersQuery(bool? deletedUsers) : PagedRequestModel, IRequest<PagedResponse<GetUsersQueryResponse>>;
