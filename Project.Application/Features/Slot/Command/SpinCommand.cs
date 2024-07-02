using MediatR;
using Project.Domain.Aggregates.Transactions;

namespace Project.Application.Features.Slot.Command;


public sealed record SpinCommand(decimal BetAmount) : IRequest<Transaction>;