namespace Slot.Shared.Aggregates;

public abstract class Entity
{
    public int Id { get; protected set; }
    public Guid SecondaryId { get; protected set; } = Guid.NewGuid();
}
