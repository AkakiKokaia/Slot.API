using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Project.Application.Configuration.Exceptions;
using Project.Application.Configuration.Exceptions.Types;
using Project.Domain.Aggregates.Transactions;
using Project.Domain.Aggregates.Transactions.Enum;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Domain.Aggregates.Users.Entity;

namespace Project.Application.Features.Account.Deposit;

public class DepositCommandHandler : IRequestHandler<DepositCommand>
{
    private readonly UserManager<User> _userManager;
    private readonly HttpContext _context;
    private readonly ITransactionRepository _transactionRepository;

    public DepositCommandHandler(UserManager<User> userManager, IHttpContextAccessor contextAccessor, ITransactionRepository transactionRepository)
    {
        _userManager = userManager;
        _context = contextAccessor.HttpContext;
        _transactionRepository = transactionRepository;
    }

    public async Task Handle(DepositCommand request, CancellationToken cancellationToken)
    {
        string userId = _context.User.Claims.First(c => c.Type == "UserID").Value;

        var user = await _userManager.FindByIdAsync(userId);

        if (user == null)
            throw new ApiException(ApiExceptionCodeTypes.NotFound);

        user.Balance += request.Amount;
        var result = await _userManager.UpdateAsync(user);

        var transaction = new Transaction(Guid.Parse(userId), request.Amount, TransactionType.Deposit, null);
        await _transactionRepository.AddAsync(transaction);
        await _transactionRepository.SaveChangesAsync(cancellationToken);
    }
}
