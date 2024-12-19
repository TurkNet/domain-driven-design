using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class ShipmentEntityConfiguration : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne<Warehouse>(i => i.SourceWarehouse)
            .WithMany(f => f.SourceShipments)
            .HasForeignKey(i => i.SourceWarehouseId)
            .IsRequired();

        builder
            .HasOne<Warehouse>(i => i.TargetWarehouse)
            .WithMany(f => f.TargetShipments)
            .HasForeignKey(i => i.TargetWarehouseId)
            .IsRequired();

        builder.Property(x => x.Description).IsRequired();
        builder.Property(x => x.ShipmentStatus).IsRequired();
        builder.Property(x => x.CompletedDate).IsRequired(false);
        builder.Property(x => x.ShippingAddress).HasColumnType("jsonb").IsRequired();
        builder.Property(x => x.ReceiverName).IsRequired();
        builder.Property(x => x.ReceiverPhoneNumber).IsRequired();

        builder.ToTable("Shipments");
    }
}