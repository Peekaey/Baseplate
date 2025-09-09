
using Baseplate.DataService;
using Microsoft.EntityFrameworkCore;

namespace Baseplate.WebApis;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Baseplate...");
        var builder = WebApplication.CreateBuilder(args);
        
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        ConfigureDatabaseServices(builder);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        InitialiseDatabase(app);

        app.Run();
    }
    
    private static void ConfigureDatabaseServices(WebApplicationBuilder builder)
    {
        string postgresConnectionString = builder.Configuration["Database:ConnectionString"];
        
        if (string.IsNullOrEmpty(postgresConnectionString))
        {
            Console.WriteLine("DatabaseConnectionString is missing");
            throw new ArgumentException("Postgres connection string is not configured.");
        }
        
        builder.Services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(postgresConnectionString, npgsqlOptions =>
            {
                npgsqlOptions.MigrationsAssembly("Baseplate.DataService");
            });
            options.UseNpgsql(postgresConnectionString)
                .LogTo(Console.WriteLine, LogLevel.Information);
        });
    }
    
    private static void InitialiseDatabase(IHost host)
    {
        using (IServiceScope scope = host.Services.CreateScope())
        {
            try
            {
                DataContext dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();
                if (!dbContext.Database.CanConnect())
                {
                    throw new ApplicationException("Unable to connect to database.");
                }

                dbContext.Database.Migrate();
                Console.WriteLine("Database initialisation complete.");
            }
            catch (Exception e)
            {
                Console.WriteLine("Error occurred while initialising database: " + e.Message);
                throw;
            }
        }
    }
}

