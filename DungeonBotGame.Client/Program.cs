using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazor.Extensions.Logging;
using DungeonBotGame.Client.BusinessLogic;
using DungeonBotGame.Client.BusinessLogic.Combat;
using DungeonBotGame.Client.BusinessLogic.Combat.AbilityProcessors;
using DungeonBotGame.Client.BusinessLogic.Combat.CombatEffectProcessors;
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
            services.AddScoped<IAbilityDescriptionProvider, AbilityDescriptionProvider>();
            services.AddScoped<IClassDetailProvider, ClassDetailProvider>();
            services.AddScoped<IActionComponentAbilityExtensionMethodsClassBuilder, ActionComponentAbilityExtensionMethodsClassBuilder>();
            services.AddScoped<IDungeonBotFactory, DungeonBotFactory>();
            services.AddScoped<IEnemyFactory, EnemyFactory>();
            services.AddScoped<IAbilityContextDictionaryBuilder, AbilityContextDictionaryBuilder>();

            services.AddScoped<ICombatEventProcessor, CharacterActionCombatEventProcessor>();
            services.AddScoped<ICombatEventProcessor, CooldownResetCombatEventProcessor>();
            services.AddScoped<ICombatEventProcessor, CombatEffectCombatEventProcessor>();

            services.AddScoped<ICombatLogEntryBuilder, CombatLogEntryBuilder>();
            services.AddScoped<ICombatDamageApplier, CombatDamageApplier>();

            services.AddScoped<IActionProcessor, AttackActionProcessor>();
            services.AddScoped<IActionProcessor, AbilityActionProcessor>();

            services.AddScoped<IAbilityProcessor, HeavySwingAbilityProcessor>();
            services.AddScoped<IAbilityProcessor, AnalyzeSituationAbilityProcessor>();
            services.AddScoped<IPassiveAbilityProcessor, SalvageStrikesAbilityProcessor>();
            services.AddScoped<IPassiveAbilityProcessor, SurpriseAttackAbilityProcessor>();

            services.AddScoped<IAbilityProcessor, RepairAbilityProcessor>();
            services.AddScoped<IPassiveAbilityProcessor, RepairAbilityProcessor>();

            services.AddScoped<IAbilityProcessor, LickWoundsAbilityProcessor>();
            services.AddScoped<IAbilityProcessor, SwipeAbilityProcessor>();
            services.AddScoped<IPassiveAbilityProcessor, ProtectBabiesAbilityProcessor>();

            services.AddScoped<ICombatEffectDirector, CombatEffectDirector>();

            services.AddScoped<IIterationsUntilNextActionCombatEffectProcessor, ActionCombatTimePercentageCombatEffectProcessor>();
            services.AddScoped<IIterationsUntilNextActionCombatEffectProcessor, ImmediateActionCombatEffectProcessor>();

            services.AddScoped<IAttackValueCombatEffectProcessor, AttackPercentageCombatEffectProcessor>();

            services.AddScoped<IBeforeActionCombatEffectProcessor, StunnedCombatEffectProcessor>();
            services.AddScoped<IBeforeActionCombatEffectProcessor, StunTargetCombatEffectProcessor>();

            services.AddScoped<IAfterDamageCombatEffectProcessor, SalvageStrikesCombatEffectProcessor>();

            services.AddScoped<IAfterActionCombatEffectProcessor, StunTargetCombatEffectProcessor>();

            services.AddScoped<ICombatEventCombatEffectProcessor, DamageOverTimeCombatEffectProcessor>();

            services.AddScoped<IAfterCharacterFallsCombatEffectProcessor, ProtectBabiesCombatEffectProcessor>();

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
