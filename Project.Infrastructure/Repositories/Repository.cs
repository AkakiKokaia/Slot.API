using MediatR;
using Microsoft.EntityFrameworkCore;
using Project.Infrastructure.Db;
using Project.Shared.Aggregates;
using Project.Shared.Interfaces;

namespace Project.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : AggregateRoot
    {
        private readonly ProjectDbContext _context;
        private readonly IIdentityService _identityService;
        private readonly IMediator _mediator;

        public Repository(
                    ProjectDbContext context,
                    IIdentityService identityService,
                    IMediator mediator)
        {
            _context = context;
            _identityService = identityService;
            _mediator = mediator;
        }

        public IQueryable<TEntity> Query()
        {
            return _context.Set<TEntity>();
        }

        public virtual async Task<TEntity> FindByIdAsync(int id)
        {
            return await _context.FindAsync<TEntity>(id);
        }

        public virtual async Task<TEntity> FindBySeconderyIdAsync(Guid id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.SecondaryId == id);
        }

        public async Task AddAsync(TEntity entity)
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entity)
        {
            await _context.Set<TEntity>().AddRangeAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _context.Set<TEntity>().Update(entity);
        }

        public void UpdateRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().UpdateRange(entity);
        }

        public void Remove(TEntity entity)
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entity)
        {
            _context.Set<TEntity>().RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var userId = _identityService.GetAuthorizedId();

            foreach (var item in _context.ChangeTracker.Entries<AggregateRoot>())
            {
                if (item is { State : EntityState.Added})
                {
                    item.Entity.AddCreationInfo(userId);
                }

                if (item is { State : EntityState.Added or EntityState.Modified})
                {
                    item.Entity.ChangeLastUpdateDate(userId);
                }
            }

            await _context.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
