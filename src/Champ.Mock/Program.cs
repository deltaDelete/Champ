using System.ComponentModel;
using Bogus;
using Champ.API;
using Champ.API.Models;
using Lunet.Extensions.Logging.SpectreConsole;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Spectre.Console;
using Spectre.Console.Cli;

namespace Champ.Mock;

public static class Program {
    public static async Task<int> Main(string[] args) {
        var conf = new Lazy<IConfiguration>(() => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build());

        var services = new ServiceCollection();
        services.AddScoped<IConfiguration>(_ => conf.Value);
        services.AddLogging(buider => buider.AddSpectreConsole(new SpectreConsoleLoggerOptions() {
            IncludeNewLineBeforeMessage = false,
            IncludeTimestamp = true,
        }));
        services.AddDbContext<ApplicationContext>((serviceProvider, options) => {
            options.UseLoggerFactory(serviceProvider.GetService<ILoggerFactory>());
        });

        var registrar = new TypeRegistrar(services);

        var app = new CommandApp(registrar);

        app.Configure(config => {
            config.AddCommand<GenerateCommand>("generate")
                .WithAlias("g")
                .WithDescription("Generate some data")
                .WithExample("generate", "Patient")
                .WithExample("generate", "Policy", "--amount", "10");
        });

        return await app.RunAsync(args);
    }
}

class GenerateCommand : Command<GenerateCommand.Settings> {
    private readonly ApplicationContext _context;
    private readonly ILogger<GenerateCommand> _logger;

    public sealed class Settings : CommandSettings {
        [CommandArgument(0, "[type]")]
        public ModelType ModelType { get; set; }

        [CommandOption("--amount|-n")]
        [DefaultValue(5)]
        [Description("Amount of generated objects")]
        public int Amount { get; init; }
    }

    public enum ModelType {
        Patient,
        Policy
    }

    public GenerateCommand(ApplicationContext context, ILogger<GenerateCommand> logger) {
        _context = context;
        _logger = logger;
    }

    public override int Execute(CommandContext context, Settings settings) {
        var parameters = new Table();
        parameters.AddColumn("Name");
        parameters.AddColumn("Value");
        parameters.AddRow("Amount", settings.Amount.ToString());
        parameters.AddRow("Type", settings.ModelType.ToString());
        _logger.LogInformationMarkup("Running with parameters:", parameters);

        return (settings.ModelType switch {
            ModelType.Patient => ExecutePatientAsync(context, settings),
            ModelType.Policy => ExecutePolicyAsync(context, settings),
            _ => throw new ArgumentException("")
        }).ConfigureAwait(false).GetAwaiter().GetResult();
    }

    public async Task<int> ExecutePatientAsync(CommandContext cmdContext, Settings settings) {
        _logger.LogInformation("Starting Patient data generation");
        var customInstantiate = new Faker<Patient>("ru")
            .CustomInstantiator(f => new Patient() {
                GenderId = f.PickRandom(1, 2),
                Address = f.Address.FullAddress(),
                DateOfBirth = f.Person.DateOfBirth,
                Email = f.Person.Email,
                FirstName = f.Person.FirstName,
                LastName = f.Person.LastName,
                MiddleName = f.PickRandom(f.Person.UserName, ""),
                Photo = GetPhoto().Result,
                PassportNumber = f.Random.Long(0L, 9999999999L),
                PhoneNumber = f.Person.Phone,
            });
        var patients = customInstantiate.GenerateLazy(settings.Amount).ToList();

        await _context.Patients.AddRangeAsync(patients);
        await _context.SaveChangesAsync();

        var medCards = patients.Select(x => new MedCard() {
            DateOfIssue = DateTimeOffset.Now,
            PatientId = x.PatientId
        });

        await _context.MedCards.AddRangeAsync(medCards);
        await _context.SaveChangesAsync();

        _logger.LogInformation("Successfully generated {} Patients", settings.Amount);

        return 0;
    }

    public async Task<int> ExecutePolicyAsync(CommandContext cmdContext, Settings settings) {
        _logger.LogInformation("Starting Policy data generation");
        var patients = _context.Patients.Select(p => p.PatientId).Take(100).ToList();
        var customInstantiate = new Faker<Policy>("ru")
            .CustomInstantiator(f => new Policy() {
                PatientId = f.PickRandom(patients),
                ExpirationDate = DateTimeOffset.Now.AddYears(8),
                InsuranceCompanyId =
                    f.PickRandom(_context.InsuranceCompanies.Select(i => i.InsuranceCompanyId).ToList()),
            });
        var policies = customInstantiate.GenerateLazy(settings.Amount);

        await _context.Policies.AddRangeAsync(policies);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Successfully generated {} Policies", settings.Amount);
        return 0;
    }

    public static async Task<byte[]> GetPhoto() {
        using var httpClient = new HttpClient();
        var bytes = await httpClient.GetByteArrayAsync("https://thispersondoesnotexist.com/");
        return bytes;
    }
}