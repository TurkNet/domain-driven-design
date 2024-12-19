using Tn.Inventory.Domain.Primitives;

namespace Tn.Inventory.Domain.Events;

public sealed record PurchaseCompletedEvent(int Id) : IDomainEvent;