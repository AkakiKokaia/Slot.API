using MediatR;

namespace Slot.Application.Features.Account.Deposit;

public sealed record DepositCommand(decimal Amount) : IRequest;