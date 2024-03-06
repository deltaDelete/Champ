using Champ.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Champ.API;

public class ApplicationContext : DbContext {
    private readonly ILogger<DbContext> _logger;

    public string ConnectionString = string.Empty;

    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<Hospitalization> Hospitalizations { get; set; } = null!;
    public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Procedure> Procedures { get; set; } = null!;
    public DbSet<Policy> Policies { get; set; } = null!;
    public DbSet<ProcedureType> ProcedureTypes { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;

    public ApplicationContext(IConfiguration configuration, ILogger<DbContext> logger) {
        _logger = logger;
        ConnectionString = configuration.GetSection("Database").GetValue<string>("ConnectionString") ?? throw new Exception("Database.ConnectionString is empty or non existent");
        Database.EnsureCreated();
        Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
        optionsBuilder.LogTo(s => _logger.LogDebug("{}", s));
    }
}