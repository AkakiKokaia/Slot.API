using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Domain.Aggregates.Transactions;
using Project.Domain.Aggregates.Transactions.Enum;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Domain.Aggregates.Users.Entity;
using Project.Shared.Interfaces;
using System.Security.Claims;

namespace Project.Application.Features.Slot.Command;

public class SpinCommandHandler : IRequestHandler<SpinCommand, Transaction>
{
    private readonly UserManager<User> _userManager;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISlotLogicService _slotLogicService;
    private readonly HttpContext _context;

    public SpinCommandHandler(UserManager<User> userManager, ITransactionRepository transactionRepository, ISlotLogicService slotLogicService, IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _transactionRepository = transactionRepository;
        _slotLogicService = slotLogicService;
        _context = contextAccessor.HttpContext;
    }

    public async Task<Transaction> Handle(SpinCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _context.User.Claims.First(c => c.Type == "UserID").Value;

        if (userIdClaim == null || !Guid.TryParse(userIdClaim, out var userId))
        {
            throw new Exception("User not found or invalid user ID in JWT token.");
        }

        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception("User not found");

        const decimal spinCost = 1.0m;

        if (user.Balance < spinCost) throw new Exception("Insufficient balance");

        user.Balance -= spinCost;

        var result = _slotLogicService.GenerateSlotResult();
        var winAmount = _slotLogicService.CalculateWinAmount(result);

        TransactionType transactionType;
        decimal transactionAmount;

        if (winAmount > 0)
        {
            transactionType = TransactionType.Won;
            transactionAmount = winAmount;
            user.Balance += winAmount;
        }
        else if (winAmount == spinCost)
        {
            transactionType = TransactionType.Draw;
            transactionAmount = 0;
        }
        else
        {
            transactionType = TransactionType.Lost;
            transactionAmount = -spinCost;
        }

        var transaction = new Transaction(userId, transactionAmount, transactionType, result);
        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync(cancellationToken);

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new Exception("Failed to update user balance");
        }

        return transaction;
    }
}
