using Champ.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Champ.API;

public class ApplicationContext : DbContext {
    private readonly ILogger<DbContext> _logger;

    public string ConnectionString = string.Empty;

    public DbSet<Department> Departments { get; set; } = null!;
    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<InsuranceCompany> InsuranceCompanies { get; set; } = null!;
    public DbSet<Hospitalization> Hospitalizations { get; set; } = null!;
    public DbSet<Measure> Measures { get; set; } = null!;
    public DbSet<MeasureType> MeasureTypes { get; set; } = null!;
    public DbSet<MedCard> MedCards { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Policy> Policies { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;

    #region Pharmacy

    public DbSet<Drug> Drugs { get; set; } = null!;
    public DbSet<DrugStock> DrugStocks { get; set; } = null!;
    public DbSet<Warehouse> Warehouses { get; set; } = null!;

    #endregion

    public ApplicationContext(IConfiguration configuration, ILogger<DbContext> logger) {
        _logger = logger;
        ConnectionString = configuration.GetSection("Database").GetValue<string>("ConnectionString") ??
                           throw new Exception("Database.ConnectionString is empty or non existent");
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        optionsBuilder.LogTo(s => _logger.LogDebug("{}", s));
    }
}
