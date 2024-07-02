using Project.Domain.Aggregates.Transactions.Enum;

namespace Project.Application.Features.Slot.Query.DataModels;

public class SpinResultDTO
{
    public decimal CurrentBalance { get; set; }
    public decimal WinAmount { get; set; }
    public decimal BetAmount { get; set; }
    public string SlotResult { get; set; }
    public TransactionType TransactionType { get; set; }
}
