namespace Slot.Shared.Aggregates;

public abstract class AggregateRoot : Entity
{
    public DateTimeOffset CreatedAt { get; protected set; } = DateTimeOffset.UtcNow;

    public Guid? CreatedBy { get; protected set; }

    public DateTimeOffset? UpdatedAt { get; protected set; }

    public Guid? UpdatedBy { get; protected set; }

    public DateTimeOffset? DeleteAt { get; protected set; }

    public Guid? DeletedBy { get; protected set; }

    public bool IsDeleted { get; protected set; }

    public void ChangeLastUpdateDate(Guid? userId)
    {
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    public void ChangeStatusAsDeleted(Guid? userId)
    {
        IsDeleted = true;
        DeleteAt = DateTimeOffset.UtcNow;
    }

    public void AddCreationInfo(Guid? userId)
    {
        CreatedBy = userId;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
