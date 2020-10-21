using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions.Logging;
using DungeonBotGame.Client.BusinessLogic;
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

            builder.Services.AddScoped<IDungeonRunner, DungeonRunner>();
            builder.Services.AddScoped<IEncounterRunner, EncounterRunner>();
            builder.Services.AddScoped<IEncounterRoundRunner, EncounterRoundRunner>();
            builder.Services.AddScoped<IActionModuleContextProvider, ActionModuleContextProvider>();
            builder.Services.AddScoped<IActionModuleExecuter, ActionModuleExecuter>();
            builder.Services.AddScoped<ICombatValueCalculator, CombatValueCalculator>();
            builder.Services.AddScoped<ICombatActionProcessor, CombatActionProcessor>();
            builder.Services.AddScoped<IAbilityDescriptionProvider, AbilityDescriptionProvider>();
            builder.Services.AddScoped<IActionComponentAbilityExtensionMethodsClassBuilder, ActionComponentAbilityExtensionMethodsClassBuilder>();

            builder.Services.AddLogging(builder => builder
                .AddBrowserConsole()
                .SetMinimumLevel(LogLevel.Information));

            builder.Services.AddFluxor(options => options.ScanAssemblies(typeof(Program).Assembly).UseReduxDevTools());

            await builder.Build().RunAsync();
        }
    }
}
