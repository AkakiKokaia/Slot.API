using MediatR;

namespace Project.Application.Features.Account.Deposit;

public sealed record DepositCommand(decimal Amount) : IRequest;