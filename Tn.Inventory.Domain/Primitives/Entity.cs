namespace Tn.Inventory.Domain.Primitives;

public abstract class Entity
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; } = false;
    public bool IsActive { get; set; } = true;
    public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public string? LastModifiedBy { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}