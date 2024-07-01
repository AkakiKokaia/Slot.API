using MediatR;
using Project.Application.Features.Users.DataModels;
using Project.Shared.Configuration.Wrappers;

namespace Project.Application.Features.Users.GetById;
public sealed record GetUserByIdQuery(Guid userId) : IRequest<Response<UserResponse>>;