using Champ.API.Models;
using Champ.API.Models.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Champ.API.Controllers;

[ApiController]
[Route("api/pharmacy/[controller]")]
public class DrugController : ControllerBase {
    private readonly IDbContextFactory<ApplicationContext> _dbContextFactory;
    private readonly ILogger<DrugController> _logger;

    public DrugController(IDbContextFactory<ApplicationContext> dbContextFactory, ILogger<DrugController> logger) {
        _dbContextFactory = dbContextFactory;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Drug>>> GetDrugs(
        CancellationToken ct,
        [FromQuery] int take = 100,
        [FromQuery] int skip = 0
    ) {
        _logger.LogInformation("Getting some drugs take={take} and skip={skip}", take, skip);
        var db = await _dbContextFactory.CreateDbContextAsync(ct);
        var list = await db.Drugs.Skip(skip).Take(take).ToListAsync(cancellationToken: ct);

        return Ok(list);
    }

    /// <summary>
    /// Количество на складах
    /// </summary>
    /// <returns>
    /// Доступность данного препарата на складах и общее количество
    /// </returns>
    [HttpGet("{id:long}/amount")]
    public async Task<ActionResult<IEnumerable<DrugAvailability>>> GetAmount(long id, CancellationToken ct) {
        var db = await _dbContextFactory.CreateDbContextAsync(ct);
        var list = await db.DrugStocks
                           .Where(x => x.DrugId == id)
                           .Include(x => x.Warehouse)
                           .GroupBy(x => x.DrugId)
                           .Select(
                               x =>
                                   new DrugAvailability(
                                       x.Key,
                                       x.Sum(stock => stock.Quantity),
                                       x.Select(stock => new WarehouseDto(stock.Warehouse!, stock.Quantity))!
                                   )
                           )
                           .ToListAsync(cancellationToken: ct);
        return Ok(list);
    }
}
