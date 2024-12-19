using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Aggregates;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class PurchaseDetailEntityConfiguration : IEntityTypeConfiguration<PurchaseDetail>
{
    public void Configure(EntityTypeBuilder<PurchaseDetail> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.LineNumber).IsRequired();

        builder
            .HasOne<Purchase>(i => i.Purchase)
            .WithMany(f => f.PurchaseDetails)
            .HasForeignKey(i => i.PurchaseId)
            .IsRequired();

        builder
            .HasOne<Device>(i => i.Device)
            .WithMany(f => f.PurchaseDetails)
            .HasForeignKey(i => i.DeviceId)
            .IsRequired();

        builder.Property(x => x.Quantity).IsRequired();

        builder.ToTable("PurchaseDetails");
    }
}