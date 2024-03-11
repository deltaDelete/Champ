using Microsoft.EntityFrameworkCore;

namespace Champ.API;

public static class Program {
    public static void Main(string[] args) {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.ConfigureServices();

        var app = builder.Build();

        app.Configure();

        app.Run();
    }

    private static void ConfigureServices(this IServiceCollection services) {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
        services.AddDbContextFactory<ApplicationContext>();
    }

    private static void Configure(this WebApplication app) {
        if (app.Environment.IsDevelopment()) {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
    }
}