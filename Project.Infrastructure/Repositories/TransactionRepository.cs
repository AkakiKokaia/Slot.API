using MediatR;
using Project.Domain.Aggregates.Transactions;
using Project.Domain.Aggregates.Transactions.Interfaces;
using Project.Infrastructure.Db;
using Project.Shared.Interfaces;

namespace Project.Infrastructure.Repositories;


public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly ProjectDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    public TransactionRepository(ProjectDbContext context, IIdentityService identityService, IMediator mediator)
        : base(context, identityService, mediator)
    {
        _context = context;
        _identityService = identityService;
        _mediator = mediator;
    }
}
