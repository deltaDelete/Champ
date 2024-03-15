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
    public async Task<IActionResult> GetAll() {
        var db = await _dbFactory.CreateDbContextAsync();
        var entities = await db.InsuranceCompanies.ToListAsync();
        return Ok(entities);
    }

    [HttpGet("{id:long}")]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(long id) {
        var db = await _dbFactory.CreateDbContextAsync();
        var entity = await db.InsuranceCompanies.FindAsync(id);
        if (entity is null) {
            return NotFound();
        }

        return Ok(entity);
    }
}