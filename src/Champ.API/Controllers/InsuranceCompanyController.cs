using Champ.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Champ.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsuranceCompanyController : ControllerBase {
    private readonly IDbContextFactory<ApplicationContext> _dbFactory;

    public InsuranceCompanyController(IDbContextFactory<ApplicationContext> dbFactory) {
        _dbFactory = dbFactory;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<InsuranceCompany>>> GetAll(CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var entities = await db.InsuranceCompanies.ToListAsync(cancellationToken: ct);
        return Ok(entities);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<InsuranceCompany>> Get(long id, CancellationToken ct) {
        var db = await _dbFactory.CreateDbContextAsync(ct);
        var entity = await db.InsuranceCompanies.FindAsync(new object?[] { id }, cancellationToken: ct);
        if (entity is null) {
            return NotFound();
        }

        return Ok(entity);
    }
}
