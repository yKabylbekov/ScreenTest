using Company.Delivery.Api.Controllers.Waybills.Request;
using Company.Delivery.Api.Controllers.Waybills.Response;
using Company.Delivery.Domain;
using Company.Delivery.Domain.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Company.Delivery.Api.Controllers.Waybills;

/// <summary>
/// Waybills management
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class WaybillsController : ControllerBase
{
    private readonly IWaybillService _waybillService;

    /// <summary>
    /// Waybills management
    /// </summary>
    public WaybillsController(IWaybillService waybillService) => _waybillService = waybillService;

    /// <summary>
    /// Получение Waybill
    /// </summary>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено или кодом 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try {
            WaybillDto waybillDto = await _waybillService.GetByIdAsync(id, cancellationToken);

            var response = new WaybillResponse
            {
                Id = waybillDto.Id,
                Number = waybillDto.Number,
                Date = waybillDto.Date,
                Items = waybillDto.Items?.Select(i => new CargoItemResponse
                {
                    Id = i.Id,
                    Name = i.Name,
                    Number = i.Number,
                    WaybillId = i.WaybillId
                })
            };

            return Ok(response);
        }
        catch ( EntityNotFoundException ) {
            return NotFound();
        }
    }

    /// <summary>
    /// Создание Waybill
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateAsync([FromBody] WaybillCreateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если успешно создано
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try {
            WaybillCreateDto createDto = new WaybillCreateDto
            {
                Number = request.Number,
                Date = request.Date,
                Items = request.Items?.Select(item => new CargoItemCreateDto
                {
                    Name = item.Name,
                    Number = item.Number
                })
            };

            var createdWaybill = await _waybillService.CreateAsync(createDto, cancellationToken);

            var response = new WaybillResponse
            {
                Id = createdWaybill.Id,
                Date = createdWaybill.Date,
                Number = createdWaybill.Number,
                Items = createdWaybill.Items?.Select(item => new CargoItemResponse
                {
                    Id = item.Id,
                    Name = item.Name,
                    Number = item.Number,
                    WaybillId = item.WaybillId
                })
            };

            return Ok(response);
        }
        catch ( EntityNotFoundException ) {
            return NotFound();
        }
    }

    /// <summary>
    /// Редактирование Waybill
    /// </summary>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(typeof(WaybillResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromBody] WaybillUpdateRequest request, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и изменено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try {
            WaybillUpdateDto updateDto = new WaybillUpdateDto
            {
                Number = request.Number,
                Date = request.Date,
                Items = request.Items?.Select(item => new CargoItemUpdateDto
                {
                    Name = item.Name,
                    Number = item.Number
                })
            };

            WaybillDto waybill = await _waybillService.UpdateByIdAsync(id, updateDto, cancellationToken);

            WaybillResponse response = new WaybillResponse
            {
                Id = waybill.Id,
                Date = waybill.Date,
                Number = waybill.Number,
                Items = waybill.Items?.Select(item => new CargoItemResponse
                {
                    Id = item.Id,
                    Number = item.Number,
                    Name = item.Name,
                    WaybillId = waybill.Id
                })
            };
            return Ok(response);

        }
        catch ( EntityNotFoundException ) {
            return NotFound();
        }
    }

    /// <summary>
    /// Удаление Waybill
    /// </summary>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        // TODO: вернуть ответ с кодом 200 если найдено и удалено, или 404 если не найдено
        // TODO: WaybillsControllerTests должен выполняться без ошибок
        try {
            await _waybillService.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch ( EntityNotFoundException ) {
            return NotFound();
        }
    }
}