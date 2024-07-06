using MediatR;
using Slot.Domain.Aggregates.Transactions;
using Slot.Domain.Aggregates.Transactions.Interfaces;
using Slot.Infrastructure.Db;
using Slot.Shared.Interfaces;

namespace Slot.Infrastructure.Repositories;


public class TransactionRepository : Repository<Transaction>, ITransactionRepository
{
    private readonly SlotDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IMediator _mediator;
    public TransactionRepository(SlotDbContext context, IIdentityService identityService, IMediator mediator)
        : base(context, identityService, mediator)
    {
        _context = context;
        _identityService = identityService;
        _mediator = mediator;
    }
}
