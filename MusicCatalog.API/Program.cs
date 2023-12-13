using Application;
using Application.ExceptionHandling;
using Carter;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Presentation;
using Serilog;

namespace MusicCatalog.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        builder.Services.AddEndpointsApiExplorer();
        
        builder.Services.AddSwaggerGen();

        builder.Services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration)
            .AddPresentation();

        builder.Host.UseSerilog((context, configuration) =>
            configuration.ReadFrom.Configuration(context.Configuration));

        builder.Services.AddCors();
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        
        app.UseMiddleware<GlobalExceptionHandlingMiddleware>();
        
        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseAuthorization();
        
        app.UseSerilogRequestLogging();
        
        app.MapCarter();
        
        app.MapGraphQL();
        
        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;

            var context = services.GetRequiredService<ApplicationDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
        }

        app.Run();
    }
}