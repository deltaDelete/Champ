using Champ.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Champ.API;

public class ApplicationContext : DbContext {

    public string ConnectionString = string.Empty;

    public DbSet<Diagnosis> Diagnoses { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;
    public DbSet<Gender> Genders { get; set; } = null!;
    public DbSet<Hospitalization> Hospitalizations { get; set; } = null!;
    public DbSet<MedicalRecord> MedicalRecords { get; set; } = null!;
    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Policy> Policies { get; set; } = null!;
    public DbSet<ProcedureType> ProcedureTypes { get; set; } = null!;
    public DbSet<Visit> Visits { get; set; } = null!;

    public ApplicationContext(IConfiguration configuration) {
        ConnectionString = configuration.GetSection("Database").GetValue<string>("ConnectionString") ?? throw new Exception("Database.ConnectionString is empty or non existent");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseMySql(ConnectionString, ServerVersion.AutoDetect(ConnectionString));
    }
}