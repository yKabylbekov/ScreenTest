using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Delivery.Database.ModelConfigurations;

internal class WaybillConfiguration : IEntityTypeConfiguration<Waybill>
{
    public void Configure(EntityTypeBuilder<Waybill> builder)
    {
        // TODO: все строковые свойства должны иметь ограничение на длину
        builder.Property(w => w.Number)
                   .HasMaxLength(50);
        // TODO: должно быть ограничение на уникальность свойства Waybill.Number
        builder.HasIndex(w => w.Number)
                   .IsUnique();
        // TODO: ApplicationDbContextTests должен выполняться без ошибок
    }
}