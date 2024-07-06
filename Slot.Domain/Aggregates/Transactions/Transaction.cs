using Slot.Domain.Aggregates.Transactions.Enum;
using Slot.Shared.Aggregates;

namespace Slot.Domain.Aggregates.Transactions;

public class Transaction : AggregateRoot
{
    public Transaction(Guid userId, decimal amount, TransactionType type, string? slotResult)
    {
        UserId = userId;
        Amount = amount;
        Type = type;
        SlotResult = slotResult;
    }
    public Guid UserId { get; set; }
    public decimal Amount { get; set; }
    public TransactionType Type { get; set; }
    public string? SlotResult { get; set; }
}