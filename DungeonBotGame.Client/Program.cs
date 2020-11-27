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
            builder.Services.AddScoped<IMetadataReferenceProvider, MetadataReferenceProvider>();

            ConfigureServices(builder.Services);

            await builder.Build().RunAsync();
        }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICSharpCompiler, CSharpCompiler>();
            services.AddScoped<ICodeCompletionService, CodeCompletionService>();

            services.AddScoped<IAdventureRunner, AdventureRunner>();
            services.AddScoped<IEncounterRunner, EncounterRunner>();
            services.AddScoped<IActionModuleContextBuilder, ActionModuleContextBuilder>();
            services.AddScoped<IActionModuleExecuter, ActionModuleExecuter>();
            services.AddScoped<ICombatValueCalculator, CombatValueCalculator>();
            services.AddScoped<ICombatActionProcessor, CombatActionProcessor>();
            services.AddScoped<IAbilityDescriptionProvider, AbilityDescriptionProvider>();
            services.AddScoped<IActionComponentAbilityExtensionMethodsClassBuilder, ActionComponentAbilityExtensionMethodsClassBuilder>();
            services.AddScoped<IDungeonBotFactory, DungeonBotFactory>();
            services.AddScoped<IEnemyFactory, EnemyFactory>();
            services.AddScoped<IAbilityContextDictionaryBuilder, AbilityContextDictionaryBuilder>();

            services.AddScoped<ICombatEventProcessor, CharacterActionCombatEventProcessor>();
            services.AddScoped<ICombatEventProcessor, CooldownResetCombatEventProcessor>();
            services.AddScoped<ICombatEventProcessor, CombatEffectCombatEventProcessor>();

            services.AddScoped<ICombatLogEntryBuilder, CombatLogEntryBuilder>();

            services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Information));

            services.AddFluxor(options => options
                .ScanAssemblies(typeof(Program).Assembly)
                .UseReduxDevTools()
                .AddMiddleware<LocalStorageMiddleware>());
        }
    }
}
