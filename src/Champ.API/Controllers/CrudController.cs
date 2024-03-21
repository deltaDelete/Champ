using Champ.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Champ.API.Controllers;

public abstract class CrudController<TKey, TValue> : ControllerBase where TValue : class {
    private readonly IDbContextFactory<ApplicationContext> _dbFactory;

    public abstract Func<TValue, TKey> KeyGetter { get; }
    public abstract Action<TValue, TKey> KeySetter { get; }

    public CrudController(IDbContextFactory<ApplicationContext> dbFactory) {
        _dbFactory = dbFactory;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TValue>>> GetAll(
        [FromQuery] int take,
        [FromQuery] int skip,
        CancellationToken ct
    ) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var entities = await db.Set<TValue>().ToListAsync(cancellationToken: ct);
        return Ok(entities);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TValue>> Get(TKey id, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var entity = await db.Set<TValue>().FindAsync(new object?[] { id }, cancellationToken: ct);
        return entity is null
                   ? NotFound(id)
                   : Ok(entity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<TValue>> Delete(TKey id, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var entity = await db.Set<TValue>().FindAsync(new object?[] { id }, cancellationToken: ct);
        if (entity is null) {
            return NotFound();
        }

        _ = db.Set<TValue>().Remove(entity);
        await db.SaveChangesAsync(ct);
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult<TValue>> Post([FromBody] TValue entity, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        KeySetter(entity, default!);
        _ = await db.Set<TValue>().AddAsync(entity, ct);
        await db.SaveChangesAsync(ct);
        return Ok(entity);
    }

    public async Task<ActionResult<TValue>> Update([FromBody] TValue entity, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        return db.Set<TValue>().Update(entity).Entity;
    }
}
