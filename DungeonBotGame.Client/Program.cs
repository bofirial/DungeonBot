using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions.Logging;
using DungeonBotGame.Client.BusinessLogic;
using DungeonBotGame.Client.BusinessLogic.Combat;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using DungeonBotGame.Client.Store;
using Fluxor;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace DungeonBotGame.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddScoped<ICSharpCompiler, CSharpCompiler>();
            builder.Services.AddScoped<ICodeCompletionService, CodeCompletionService>();

            builder.Services.AddScoped<IAdventureRunner, AdventureRunner>();
            builder.Services.AddScoped<IEncounterRunner, EncounterRunner>();
            builder.Services.AddScoped<IActionModuleContextBuilder, ActionModuleContextBuilder>();
            builder.Services.AddScoped<IActionModuleExecuter, ActionModuleExecuter>();
            builder.Services.AddScoped<ICombatValueCalculator, CombatValueCalculator>();
            builder.Services.AddScoped<ICombatActionProcessor, CombatActionProcessor>();
            builder.Services.AddScoped<IAbilityDescriptionProvider, AbilityDescriptionProvider>();
            builder.Services.AddScoped<IActionComponentAbilityExtensionMethodsClassBuilder, ActionComponentAbilityExtensionMethodsClassBuilder>();
            builder.Services.AddScoped<IDungeonBotFactory, DungeonBotFactory>();
            builder.Services.AddScoped<IEnemyFactory, EnemyFactory>();
            builder.Services.AddScoped<IAbilityContextDictionaryBuilder, AbilityContextDictionaryBuilder>();

            builder.Services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Information));

            builder.Services.AddFluxor(options => options
                .ScanAssemblies(typeof(Program).Assembly)
                .UseReduxDevTools()
                .AddMiddleware<LocalStorageMiddleware>());

            await builder.Build().RunAsync();
        }
    }
}
