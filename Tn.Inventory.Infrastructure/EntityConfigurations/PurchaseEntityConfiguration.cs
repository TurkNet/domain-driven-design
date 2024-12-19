using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class PurchaseEntityConfiguration : IEntityTypeConfiguration<Purchase>
{
    public void Configure(EntityTypeBuilder<Purchase> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne<Supplier>(i => i.Supplier)
            .WithMany(f => f.Purchases)
            .HasForeignKey(i => i.SupplierId)
            .IsRequired();

        builder.Property(x => x.PurchaseStatus).IsRequired();
        builder.Property(x => x.OrderNumber).IsRequired();
        builder.Property(x => x.DeliveryDate).IsRequired(false);
        builder.Property(x => x.TargetWarehouseId).IsRequired(false);

        builder.ToTable("Purchases");
    }
}