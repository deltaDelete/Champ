using System.Net.Mime;
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

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id) {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.Patients.FindAsync(id);
        if (entity is null) {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> Delete(long id) {
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

        entity.PatientId = 0;

        _ = await db.Patients.AddAsync(entity);
        await db.SaveChangesAsync();

        var medCard = new MedCard() {
            PatientId = entity.PatientId,
        };

        _ = await db.MedCards.AddAsync(medCard);
        await db.SaveChangesAsync();

        _logger.LogDebug("Create Patient with {}", entity.PatientId);
        _logger.LogDebug("Create MedCard with Id={MedCardId}, PatientId={PatientId}, Date={DateOfIssue}",
            medCard.MedCardId,
            medCard.PatientId,
            medCard.DateOfIssue
        );
        return Ok(entity);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> Put([FromBody] Patient entity, long id) {
        var db = await _dbFactory.CreateDbContextAsync();
        if (entity.PatientId != id) {
            entity.PatientId = id;
        }

        db.Patients.Update(entity);
        await db.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPost("{id:long}/policy")]
    public async Task<IActionResult> PostPolicy([FromBody] Policy entity, long id, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var exists = await db.Patients.Include(x => x.Policies)
            .FirstOrDefaultAsync(x => x.PatientId == id, ct);
        
        if (exists is null) {
            return BadRequest();
        }

        exists.Policies?.Add(entity);

        await db.SaveChangesAsync(ct);

        _logger.LogDebug("Created Policy with Id={PolicyId}, PatientId={PatientId}", entity.PolicyId, entity.PatientId);
        return Ok(entity);
    }

    [HttpPost("{id:long}/photo")]
    public async Task<IActionResult> PostPhoto([FromForm] IFormFile file, long id, CancellationToken ct) {
        if (file.ContentType == "image/jpeg") {
            return BadRequest("Только изображения jpg/jpeg");
        }
        if (file.Length > 16777216L) {
            return BadRequest("Файл больше 16MB");
        }
        
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var exists = await db.Patients.FindAsync(new object?[] { id }, cancellationToken: ct);
        
        if (exists is null) {
            return NotFound(id);
        }

        await using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream, ct);

        exists.Photo = memoryStream.ToArray();

        await db.SaveChangesAsync(ct);
        _logger.LogDebug("Photo was uploaded for Patient {}", exists.PatientId);
        return Ok();
    }

    [HttpGet("{id:long}/photo")]
    public async Task<IActionResult> GetPhoto([FromRoute] long id, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var exists = await db.Patients.FindAsync(new object?[] { id }, cancellationToken: ct);
        if (exists is null) {
            return NotFound(id);
        }

        var cd = new ContentDisposition() {
            Inline = true,
            FileName = $"{id}.jpg",
            Size = exists.Photo.Length,
        };
        Response.Headers.Add("Content-Disposition", cd.ToString());
        Response.Headers.Add("X-Content-Type-Options", "nosniff");
        
        return File(exists.Photo, "image/jpeg");
    }
}