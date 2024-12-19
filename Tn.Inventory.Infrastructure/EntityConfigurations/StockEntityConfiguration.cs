using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class StockEntityConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne<Warehouse>(i => i.Warehouse)
            .WithMany(f => f.Stocks)
            .HasForeignKey(i => i.WarehouseId)
            .IsRequired();

        builder
            .HasOne<Device>(i => i.Device)
            .WithMany(f => f.Stocks)
            .HasForeignKey(i => i.DeviceId)
            .IsRequired();

        builder.Property(x => x.Quantity).IsRequired();

        builder.ToTable("Stocks");
    }
}