using MediatR;

namespace Slot.Application.Features.Account.Withdraw;

public sealed record WithdrawCommand(decimal Amount) : IRequest<Unit>;