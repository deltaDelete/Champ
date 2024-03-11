using Champ.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Champ.API.Controllers;

[ApiController]
[Route("/api/[controller]")]
public class PatientController : ControllerBase {
    private readonly IDbContextFactory<ApplicationContext> _dbFactory;
    private readonly ILogger<PatientController> _logger;

    public PatientController(IDbContextFactory<ApplicationContext> dbFactory, ILogger<PatientController> logger) {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll() {
        var db = await _dbFactory.CreateDbContextAsync();
        var entities = await db.Patients.ToListAsync();
        return Ok(entities);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id) {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.Patients.FindAsync(id);
        if (entity is null) {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id) {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.Patients.FindAsync(id);
        if (entity is null) {
            return NotFound();
        }

        _ = db.Patients.Remove(entity);
        await db.SaveChangesAsync();

        return Ok(entity);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Patient entity) {
        var db = await _dbFactory.CreateDbContextAsync();
        var exists = db.Patients.Contains(entity);
        if (exists) {
            return BadRequest();
        }
        // todo create medcard

        _ = await db.Patients.AddAsync(entity);
        await db.SaveChangesAsync();

        var medcard = new MedCard() {
            PatientId = entity.PatientId,
        };

        _ = await db.MedCards.AddAsync(medcard);
        await db.SaveChangesAsync();

        _logger.LogInformation("Create Patient with {}", entity.PatientId);
        _logger.LogInformation("Create MedCard with Id={MedCardId}, PatientId={PatientId}, Date={DateOfIssue}",
            medcard.MedCardId,
            medcard.PatientId,
            medcard.DateOfIssue
        );
        return Ok(entity);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Put([FromBody] Patient entity, int id) {
        var db = await _dbFactory.CreateDbContextAsync();
        if (entity.PatientId != id) {
            entity.PatientId = id;
        }

        db.Patients.Update(entity);
        await db.SaveChangesAsync();
        return Ok(entity);
    }
}