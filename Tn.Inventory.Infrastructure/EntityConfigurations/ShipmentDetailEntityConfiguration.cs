using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class ShipmentDetailEntityConfiguration : IEntityTypeConfiguration<ShipmentDetail>
{
    public void Configure(EntityTypeBuilder<ShipmentDetail> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.LineNumber).IsRequired();

        builder
            .HasOne<Shipment>(i => i.Shipment)
            .WithMany(f => f.ShipmentDetails)
            .HasForeignKey(i => i.ShipmentId)
            .IsRequired();

        builder
            .HasOne<Device>(i => i.Device)
            .WithMany(f => f.ShipmentDetails)
            .HasForeignKey(i => i.DeviceId)
            .IsRequired();

        builder.Property(x => x.Quantity).IsRequired();

        builder.ToTable("ShipmentDetails");
    }
}