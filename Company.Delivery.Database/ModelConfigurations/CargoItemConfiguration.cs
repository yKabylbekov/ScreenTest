using Company.Delivery.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Delivery.Database.ModelConfigurations;

internal class CargoItemConfiguration : IEntityTypeConfiguration<CargoItem>
{
    public void Configure(EntityTypeBuilder<CargoItem> builder)
    {
        // TODO: все строковые свойства должны иметь ограничение на длину
        builder.Property(ci => ci.Number).HasMaxLength(50);
        // TODO: должно быть ограничение на уникальность свойства CargoItem.Number в пределах одной сущности Waybill
        builder.HasIndex(ci => new { ci.Number, ci.WaybillId }).IsUnique();

        builder.HasOne(ci => ci.Waybill)
               .WithMany(wb => wb.Items)
               .HasForeignKey(ci => ci.WaybillId)
               .OnDelete(DeleteBehavior.Cascade);
        // TODO: ApplicationDbContextTests должен выполняться без ошибок
    }
}