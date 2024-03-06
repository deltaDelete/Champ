using Champ.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Champ.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class GenderController : ControllerBase
{
    private readonly IDbContextFactory<ApplicationContext> _dbFactory;

    public GenderController(IDbContextFactory<ApplicationContext> dbFactory)
    {
        _dbFactory = dbFactory;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var entities = await db.Genders.ToListAsync();
        return Ok(entities);
    }
    
    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.Genders.FindAsync(id);
        if (entity is null)
        {
            return NotFound(NotFound);
        }

        return Ok(entity);
    }
    
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.Genders.FindAsync(id);
        if (entity is null)
        {
            return NotFound(NotFound);
        }

        _ = db.Genders.Remove(entity);
        await db.SaveChangesAsync();

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Gender entity)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        var exists = db.Genders.Contains(entity);
        if (exists)
        {
            return BadRequest(AlreadyPresent);
        }

        await db.Genders.AddAsync(entity);
        await db.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromBody] Gender entity, int id)
    {
        var db = await _dbFactory.CreateDbContextAsync();
        if (entity.GenderId != id)
        {
            entity.GenderId = id;
        }

        db.Genders.Update(entity);
        await db.SaveChangesAsync();
        return Ok(entity);
    }

    private const string AlreadyPresent = "Запись уже существует";
    private const string NotFound = "Запись с этим id не найдена";
}