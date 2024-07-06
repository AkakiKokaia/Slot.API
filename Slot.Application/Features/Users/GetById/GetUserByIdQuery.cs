using MediatR;
using Slot.Application.Features.Users.DataModels;
using Slot.Shared.Configuration.Wrappers;

namespace Slot.Application.Features.Users.GetById;
public sealed record GetUserByIdQuery(Guid userId) : IRequest<Response<UserResponse>>;