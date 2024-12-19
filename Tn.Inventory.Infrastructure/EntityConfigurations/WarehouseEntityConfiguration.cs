using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class WarehouseEntityConfiguration : IEntityTypeConfiguration<Warehouse>
{
    public void Configure(EntityTypeBuilder<Warehouse> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name).IsRequired();
        builder
            .HasOne<Supplier>(i => i.Supplier)
            .WithMany(f => f.Warehouses)
            .HasForeignKey(i => i.SupplierId)
            .IsRequired();

        builder.Property(x => x.Address).HasColumnType("jsonb").IsRequired();

        builder.ToTable("Warehouses");
    }
}