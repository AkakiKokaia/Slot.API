using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Slot.Application.Features.Slot.Query.DataModels;
using Slot.Application.Hubs;
using Slot.Domain.Aggregates.Transactions;
using Slot.Domain.Aggregates.Transactions.Enum;
using Slot.Domain.Aggregates.Transactions.Interfaces;
using Slot.Domain.Aggregates.Users.Entity;
using Slot.Shared.Interfaces;

namespace Slot.Application.Features.Slot.Command;

public class SpinCommandHandler : IRequestHandler<SpinCommand, SpinResultDTO>
{
    private readonly UserManager<User> _userManager;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ISlotLogicService _slotLogicService;
    private readonly HttpContext _context;
    private readonly IHubContext<SlotHub> _hubContext;

    public SpinCommandHandler(UserManager<User> userManager, ITransactionRepository transactionRepository, ISlotLogicService slotLogicService, IHttpContextAccessor contextAccessor, IHubContext<SlotHub> hubContext)
    {
        _userManager = userManager;
        _transactionRepository = transactionRepository;
        _slotLogicService = slotLogicService;
        _context = contextAccessor.HttpContext;
        _hubContext = hubContext;
    }

    public async Task<SpinResultDTO> Handle(SpinCommand request, CancellationToken cancellationToken)
    {
        string userIdClaim = _context.User.Claims.First(c => c.Type == "UserID").Value;

        if (userIdClaim == default || !Guid.TryParse(userIdClaim, out var userId))
        {
            throw new Exception("User not found or invalid user ID in JWT token.");
        }

        var user = await _userManager.FindByIdAsync(userId.ToString()) ?? throw new Exception("User not found");

        var betAmount = request.BetAmount;

        if (user.Balance < betAmount) throw new Exception("Insufficient balance");

        user.Balance -= betAmount;

        var result = _slotLogicService.GenerateSlotResult();
        var winAmount = _slotLogicService.CalculateWinAmount(result, betAmount);

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
            transactionAmount = -betAmount;
        }

        var resultString = _slotLogicService.ConvertResultToString(result);
        var transaction = new Transaction(userId, transactionAmount, transactionType, resultString);
        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync(cancellationToken);

        var updateResult = await _userManager.UpdateAsync(user);
        if (!updateResult.Succeeded)
        {
            throw new Exception("Failed to update user balance");
        }

        var spinResult = new SpinResultDTO
        {
            CurrentBalance = user.Balance,
            WinAmount = winAmount,
            BetAmount = betAmount,
            SlotResult = resultString,
            TransactionType = transactionType
        };

        return spinResult;
    }
}
