using System.Net.Mime;
using Champ.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

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
    public async Task<ActionResult<IEnumerable<Patient>>> GetAll() {
        var db = await _dbFactory.CreateDbContextAsync();
        var entities = await db.Patients.ToListAsync();
        return Ok(entities);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
    public async Task<ActionResult<Patient>> Get(long id) {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.Patients.FindAsync(id);
        if (entity is null) {
            return NotFound();
        }

        return Ok(entity);
    }

    [HttpDelete("{id:long}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Patient>> Delete(long id) {
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
    public async Task<ActionResult<Patient>> Post([FromBody] Patient entity) {
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

    [HttpPost("register")]
    public async Task<ActionResult<Patient>> Post([FromBody] PatientRegister register, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);

        var entity = register.Patient;

        entity.PatientId = 0;

        _ = await db.Patients.AddAsync(entity, ct);
        await db.SaveChangesAsync(ct);

        var medCard = new MedCard() {
            PatientId = entity.PatientId,
        };

        _ = await db.MedCards.AddAsync(medCard, ct);
        await db.SaveChangesAsync(ct);

        _logger.LogDebug("Created Patient with {}", entity.PatientId);
        _logger.LogDebug("Created MedCard with Id={MedCardId}, PatientId={PatientId}, Date={DateOfIssue}",
            medCard.MedCardId,
            medCard.PatientId,
            medCard.DateOfIssue
        );

        var policy = register.Policy;
        var policyInDb = await db.Policies.FindAsync(new object?[] { policy.PolicyId }, cancellationToken: ct);
        if (policyInDb is not null) {
            return BadRequest("Пациент с этим полисом уже существует");
        }

        policy.PatientId = entity.PatientId;
         _ = await db.Policies.AddAsync(register.Policy, ct);

        await db.SaveChangesAsync(ct);

        _logger.LogDebug(
            "Created Policy with PolicyId={PolicyId}, PatientId={PatientId}, InsuranceCompanyId={InsuranceCompanyId}",
            policy.PolicyId, policy.PatientId, policy.InsuranceCompanyId);

        return Ok(entity);
    }

    public class PatientRegister {
        public Patient Patient { get; set; }
        public Policy Policy { get; set; }
    }

    [HttpPut("{id:long}")]
    public async Task<ActionResult<Patient>> Put([FromBody] Patient entity, long id) {
        var db = await _dbFactory.CreateDbContextAsync();
        if (entity.PatientId != id) {
            entity.PatientId = id;
        }

        db.Patients.Update(entity);
        await db.SaveChangesAsync();
        return Ok(entity);
    }

    [HttpPost("{id:long}/policy")]
    public async Task<ActionResult<Policy>> PostPolicy([FromBody] Policy entity, long id, CancellationToken ct) {
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
    public async Task<IActionResult> PostPhoto([FromForm(Name =  "photo")] IFormFile file, long id, CancellationToken ct) {
        if (file.ContentType != "image/jpeg") {
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

        var medCard = await db.MedCards.FirstOrDefaultAsync(x => x.PatientId == id, cancellationToken: ct);
        if (medCard is not null) {
            medCard.Photo = exists.Photo;
        }

        return Ok();
    }

    [HttpGet("{id:long}/photo")]
    [ProducesResponseType(typeof(FileContentResult), 200)]
    [ProducesResponseType(typeof(NotFoundObjectResult), 404)]
    public async Task<ActionResult> GetPhoto([FromRoute] long id, CancellationToken ct) {
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