using Microsoft.EntityFrameworkCore;

namespace Company.Delivery.Core;

[Index(nameof(Number), IsUnique = true)]
public class Waybill
{
    public Guid Id { get; set; }

    // TODO: уникальное значение
    public string Number { get; set; } = null!;

    public DateTime Date { get; set; }

    public ICollection<CargoItem>? Items { get; set; }
}