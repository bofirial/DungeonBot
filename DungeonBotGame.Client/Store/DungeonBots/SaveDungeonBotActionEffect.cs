using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using DungeonBotGame.Models.ViewModels;
using Fluxor;
using Microsoft.Extensions.Logging;

namespace DungeonBotGame.Client.Store.DungeonBots
{
    public class SaveDungeonBotActionEffect : Effect<SaveDungeonBotAction>
    {
        private readonly ICSharpCompiler _cSharpCompiler;
        private readonly ILogger<SaveDungeonBotActionEffect> _logger;

        public SaveDungeonBotActionEffect(ICSharpCompiler cSharpCompiler, ILogger<SaveDungeonBotActionEffect> logger)
        {
            _cSharpCompiler = cSharpCompiler;
            _logger = logger;
        }
        protected override async Task HandleAsync(SaveDungeonBotAction action, IDispatcher dispatcher)
        {
            var cSharpCompilation = await _cSharpCompiler.CompileAsync(action.Code, action.DungeonBot);

            var errorDiagnostics = cSharpCompilation.GetDiagnostics().Where(x => x.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error);
            if (errorDiagnostics.Any())
            {
                foreach (var diagnostic in errorDiagnostics)
                {
                    _logger.LogInformation(diagnostic.ToString());
                }
            }

            using var compiledLibraryStream = new MemoryStream();

            var emitResult = cSharpCompilation.Emit(compiledLibraryStream);

            if (emitResult.Success)
            {
                var assembly = Assembly.Load(compiledLibraryStream.ToArray());

                if (assembly == null)
                {
                    throw new Exception("Null Assembly");
                }

                var updateLibraryAction = new UpdateActionModuleLibraryAction(action.DungeonBot.Name, action.DungeonBot.Name, compiledLibraryStream.ToArray(), new List<ActionModuleFileViewModel>() {
                        new ActionModuleFileViewModel("DungeonBotGame.cs", action.Code)
                    });

                dispatcher.Dispatch(updateLibraryAction);
            }
        }
    }
}
