using Project.Shared.Aggregates;

namespace Project.Shared.Interfaces;

public interface IRepository<TEntity> where TEntity : AggregateRoot
{
    IQueryable<TEntity> Query();

    Task<TEntity> FindByIdAsync(int id);

    Task<TEntity> FindBySeconderyIdAsync(Guid id);

    Task AddAsync(TEntity entity);

    Task AddRangeAsync(IEnumerable<TEntity> entity);

    void Update(TEntity entity);

    void UpdateRange(IEnumerable<TEntity> entity);

    void Remove(TEntity entity);

    void RemoveRange(IEnumerable<TEntity> entity);

    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}