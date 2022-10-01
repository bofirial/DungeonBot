using DungeonBotGame.Combat;
using DungeonBotGame.Data;
using DungeonBotGame.Store;
using Fluxor;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DungeonBotGame;

public static class Application
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddScoped<IDungeonBotClassificationDisplayNameProvider, DungeonBotClassificationDisplayNameProvider>();
        services.AddScoped<IAdventureRunner, AdventureRunner>();
    }

    public static void Run(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddRazorPages();
        builder.Services.AddServerSideBlazor().AddCircuitOptions(options => options.DetailedErrors = true);

        builder.Services.AddFluxor(options => options
            .ScanAssemblies(typeof(Application).Assembly)
            .UseReduxDevTools()
            .AddMiddleware<GameStateFileMiddleware>());

        ConfigureServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.MapBlazorHub();
        app.MapFallbackToPage("/_Host");

        app.Run();

    }
}
