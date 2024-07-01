using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Transactions;
using Project.Domain.Aggregates.Transactions.Enum;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Application.Features.Account.Withdraw;

public class WithdrawCommandHandler : IRequestHandler<WithdrawCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly HttpContext _context;
    private readonly ITransactionRepository _transactionRepository;

    public WithdrawCommandHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor, ITransactionRepository transactionRepository)
    {
        _userManager = userManager;
        _context = contextAccessor.HttpContext;
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(WithdrawCommand request, CancellationToken cancellationToken)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        if (user.Balance < request.Amount)
            throw new ApiException(ApiExceptionCodeTypes.InsufficientFunds, "Insufficient balance.");

        user.Balance -= request.Amount;
        var result = await _userManager.UpdateAsync(user);

        var transaction = new Transaction(Guid.Parse(userId), -request.Amount, TransactionType.Withdrawal, null);
        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync(cancellationToken);
    }
}
