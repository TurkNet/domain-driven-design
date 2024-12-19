namespace Tn.Inventory.Domain.Primitives;

public abstract class Aggregate : Entity
{
    private readonly List<IDomainEvent> _domainEvents;

    protected Aggregate()
    {
        _domainEvents = new List<IDomainEvent>();
    }

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void ClearDomainEvents()
    {
        _domainEvents?.Clear();
    }

    protected void RaiseEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
}