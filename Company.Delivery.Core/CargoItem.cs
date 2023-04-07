using System.ComponentModel.DataAnnotations;

namespace Company.Delivery.Core;

public class CargoItem
{
    public Guid Id { get; set; }

    public Guid WaybillId { get; set; }

    public Waybill? Waybill { get; set; }

    public string Number { get; set; } = null!;

    [MaxLength(50)]
    public string Name { get; set; } = null!;
}