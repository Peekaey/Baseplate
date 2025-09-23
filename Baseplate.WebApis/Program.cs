
using Baseplate.BusinessService.DatabaseServices;
using Baseplate.BusinessService.Interfaces;
using Baseplate.DataService;
using Baseplate.DataService.Interfaces;
using Baseplate.DataService.Services;
using Baseplate.Messaging;
using Baseplate.Messaging.Interfaces;
using Baseplate.Models;
using Basesplate.BackgroundService.Jobs;
using Microsoft.EntityFrameworkCore;
using Quartz;

namespace Baseplate.WebApis;
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Starting Baseplate...");
        var builder = WebApplication.CreateBuilder(args);
        BindConfigs(builder);
        RegisterBackgroundJobs(builder);
        RegisterApplicationServices(builder);
        // Add services to the container.
        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        
        // SignalR config
        builder.Services.AddSignalR(options =>
        {
            options.EnableDetailedErrors = true;
            options.KeepAliveInterval = TimeSpan.FromSeconds(15);
            options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
        });

        ConfigureDatabaseServices(builder);

        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowReact", policy =>
            {
                policy.WithOrigins("http://localhost:3000",
                        "http://localhost:3001",
                        "http://localhost:3002",
                        "http://localhost:3003",
                        "http://localhost:3004",
                        "http://localhost:3005",
                        "http://localhost:3006",
                        "http://localhost:3007"
                    ).AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
        var app = builder.Build();
        app.UseCors("AllowReact");
        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }

        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        app.MapHub<MessageHub>("/messageHub");
        InitialiseDatabase(app);

        app.Run();
    }

    private static void RegisterApplicationServices(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IRoomBusinessService, RoomBusinessService>();
        builder.Services.AddScoped<IRoomService, RoomService>();
        builder.Services.AddScoped<IMessageBusinessService, MessageBusinessService>();
        builder.Services.AddScoped<IMessageService, MessageService>();
        builder.Services.AddScoped<IAttachmentBusinessService, AttachmentBusinessService>();
        builder.Services.AddScoped<IAttachmentService, AttachmentService>();
        builder.Services.AddSingleton<IMessageHub, MessageHub>();
        builder.Services.AddQuartzHostedService(q =>
        {
            q.AwaitApplicationStarted = true;
            q.WaitForJobsToComplete = true;
        });
    }

    private static void RegisterBackgroundJobs(WebApplicationBuilder builder)
    {
        
        builder.Services.AddQuartz(q => 
        { 
            var jobKey = new JobKey("DeleteStaleChatrooms");
            q.AddJob<DeleteStaleChatrooms>(opts => opts.WithIdentity(jobKey));
            
            q.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity("DeleteStaleChatrooms-trigger")
                .WithCronSchedule("0 0 23 ? * SUN")
                .WithDescription("Runs every 7 days at 11 PM"));

        });
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

    private static void BindConfigs(WebApplicationBuilder builder)
    {
        builder.Services.Configure<QuartzJobsConfig>(
            builder.Configuration.GetSection(QuartzJobsConfig.SectionName));
        
        builder.Services.Configure<DatabaseConfig>(builder.Configuration.GetSection("Database"));
    }
}

