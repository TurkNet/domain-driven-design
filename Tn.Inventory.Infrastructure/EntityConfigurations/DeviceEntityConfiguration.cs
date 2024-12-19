using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tn.Inventory.Domain.Entities;

namespace Tn.Inventory.Infrastructure.EntityConfigurations;

public class DeviceEntityConfiguration : IEntityTypeConfiguration<Device>
{
    public void Configure(EntityTypeBuilder<Device> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder
            .HasOne<Brand>(i => i.Brand)
            .WithMany(f => f.Devices)
            .HasForeignKey(i => i.BrandId)
            .IsRequired();

        builder.Property(x => x.Code).IsRequired();
        builder.Property(x => x.Name).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(250);
        builder.Property(x => x.Price).IsRequired();

        builder.ToTable("Devices");
    }
}