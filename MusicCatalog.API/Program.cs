using Application;
using Carter;
using Infrastructure;
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
        
        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler("/error");
        
        app.UseHttpsRedirection();

        app.UseAuthentication();
        
        app.UseAuthorization();
        
        app.UseSerilogRequestLogging();
        
        app.MapCarter();
        
        app.MapGraphQL();

        app.Run();
    }
}