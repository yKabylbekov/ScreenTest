using Company.Delivery.Database;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;

namespace Company.Delivery.Infrastructure;

public class WaybillService : IWaybillService
{
    private readonly DeliveryDbContext _dbContext;

    public WaybillService(DeliveryDbContext dbContext)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<WaybillDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var waybill = await _dbContext.Waybills.FindAsync(new object[] { id }, cancellationToken);

        if ( waybill == null ) {
            throw new EntityNotFoundException();
        }

        WaybillDto waybillDto = new WaybillDto
        {
            Id = waybill.Id,
            Number = waybill.Number,
            Date = waybill.Date,
            Items = waybill.Items?.Select(item =>
            new CargoItemDto
            {
                Name = item.Name,
                Id = item.Id,
                Number = item.Number,
                WaybillId = waybill.Id
            })
        };
        return waybillDto;
    }

    public Task<WaybillDto> CreateAsync(WaybillCreateDto data, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<WaybillDto> UpdateByIdAsync(Guid id, WaybillUpdateDto data, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var waybill = await _dbContext.Waybills.FindAsync(new object[] { id }, cancellationToken);

        if ( waybill == null ) {
            throw new EntityNotFoundException();
        }

        waybill.Number = data.Number;
        waybill.Date = data.Date;

        _dbContext.Waybills.Update(waybill);

        return new WaybillDto
        {
            Id = waybill.Id,
            Number = waybill.Number,
            Date = waybill.Date,
            Items = waybill.Items?.Select(item => new CargoItemDto
            {
                Id = item.Id,
                WaybillId = waybill.Id,
                Name = item.Name,
                Number = item.Number
            })
        };
    }

    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: Если сущность не найдена по идентификатору, кинуть исключение типа EntityNotFoundException
        var waybill = await _dbContext.Waybills.FindAsync(new object[] { id }, cancellationToken);

        if ( waybill == null ) {
            throw new EntityNotFoundException();
        }

        _dbContext.Waybills.Remove(waybill);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}