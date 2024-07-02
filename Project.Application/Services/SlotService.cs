using Microsoft.AspNetCore.Identity;
using Project.Domain.Aggregates.Transactions;
using Project.Domain.Aggregates.Transactions.Enum;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Domain.Aggregates.Users.Entity;
using Project.Shared.Interfaces;

namespace Project.Application.Services;

public class SlotService : ISlotService
{
    private readonly UserManager<User> _userManager;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISlotLogicService _slotLogic;

    public SlotService(UserManager<User> userManager, ITransactionRepository transactionRepository, ISlotLogicService slotLogic)
    {
        _userManager = userManager;
        _transactionRepository = transactionRepository;
        _slotLogic = slotLogic;
    }

    public async Task<Transaction> SpinAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) throw new Exception("User not found");

        const decimal spinCost = 1.0m;
        if (user.Balance < spinCost) throw new Exception("Insufficient balance");

        user.Balance -= spinCost;

        var result = _slotLogic.GenerateSlotResult();
        var winAmount = _slotLogic.CalculateWinAmount(result, spinCost);

        TransactionType transactionType;
        decimal transactionAmount;

        if (winAmount > 0)
        {
            transactionType = TransactionType.Won;
            transactionAmount = winAmount;
            user.Balance += winAmount;
        }
        else
        {
            transactionType = TransactionType.Lost;
            transactionAmount = -spinCost;
        }

        var resultString = _slotLogic.ConvertResultToString(result);
        var transaction = new Transaction(userId, transactionAmount, transactionType, resultString);
        await _transactionRepository.AddAsync(transaction);
        await _userManager.UpdateAsync(user);

        return transaction;
    }
}
