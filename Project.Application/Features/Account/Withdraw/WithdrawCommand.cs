using MediatR;

namespace Project.Application.Features.Account.Withdraw;

public sealed record WithdrawCommand(decimal Amount) : IRequest;