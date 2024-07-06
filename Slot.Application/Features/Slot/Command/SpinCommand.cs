using MediatR;
using Slot.Application.Features.Slot.Query.DataModels;
using Slot.Domain.Aggregates.Transactions;

namespace Slot.Application.Features.Slot.Command;


public sealed record SpinCommand(decimal BetAmount) : IRequest<SpinResultDTO>;