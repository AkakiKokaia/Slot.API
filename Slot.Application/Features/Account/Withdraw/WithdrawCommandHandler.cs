using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Slot.Application.Common.Handlers;
using Slot.Application.Configuration.Exceptions;
using Slot.Application.Configuration.Exceptions.Types;
using Slot.Domain.Aggregates.Transactions;
using Slot.Domain.Aggregates.Transactions.Enum;
using Slot.Domain.Aggregates.Transactions.Interfaces;
using Slot.Domain.Aggregates.Users.Entity;

namespace Slot.Application.Features.Account.Withdraw;

public class WithdrawCommandHandler : CustomCommandHandler<WithdrawCommand, Unit>
{
    private readonly UserManager<User> _userManager;
    private readonly HttpContext _context;
    private readonly ITransactionRepository _transactionRepository;

    public WithdrawCommandHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor, ITransactionRepository transactionRepository)
        : base(userManager, contextAccessor)
    {
        _userManager = userManager;
        _context = contextAccessor.HttpContext;
        _transactionRepository = transactionRepository;
    }

    public override async Task<Unit> Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        var user = await GetUserAsync();

        if (user.Balance < request.Amount)
            throw new ApiException(ApiExceptionCodeTypes.InsufficientFunds, "Insufficient balance.");

        user.Balance -= request.Amount;
        var result = await _userManager.UpdateAsync(user);

        var transaction = new Transaction(Guid.Parse(UserId), -request.Amount, TransactionType.Withdrawal, null);
        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
