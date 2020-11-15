using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DungeonBotGame.Models.Combat;
using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public interface IActionModuleContextBuilder
    {
        Task<DungeonBotViewModel> BuildActionModuleContextAsync(DungeonBotViewModel dungeonBot);
    }

    public class ActionModuleContextBuilder : IActionModuleContextBuilder
    {
        private readonly ICSharpCompiler _cSharpCompiler;

        public ActionModuleContextBuilder(ICSharpCompiler cSharpCompiler)
        {
            _cSharpCompiler = cSharpCompiler;
        }

        public async Task<DungeonBotViewModel> BuildActionModuleContextAsync(DungeonBotViewModel dungeonBot)
        {
            var cSharpCompilation = await _cSharpCompiler.CompileAsync(dungeonBot);

            var errors = new List<ErrorViewModel>();

            var errorDiagnostics = cSharpCompilation.GetDiagnostics().Where(x => x.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error);

            if (errorDiagnostics.Any())
            {
                errors.AddRange(errorDiagnostics.Select(e => new ErrorViewModel(e.ToString())));

                return dungeonBot with { Errors = errors.ToImmutableList(), ActionModuleContext = null };
            }

            using var compiledLibraryStream = new MemoryStream();

            var emitResult = cSharpCompilation.Emit(compiledLibraryStream);

            if (!emitResult.Success)
            {
                var emitErrors = emitResult.Diagnostics.Where(x => x.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error);

                errors.AddRange(emitErrors.Select(e => new ErrorViewModel(e.ToString())));

                return dungeonBot with { Errors = errors.ToImmutableList(), ActionModuleContext = null };
            }

            var assembly = Assembly.Load(compiledLibraryStream.ToArray());

            if (assembly == null)
            {
                errors.Add(new ErrorViewModel("Unable to load the assembly."));

                return dungeonBot with { Errors = errors.ToImmutableList(), ActionModuleContext = null };
            }

            var methods = assembly.GetTypes().SelectMany(t => t.GetMethods()).Where(m => m.GetCustomAttributes(typeof(ActionModuleEntrypointAttribute), false).Length > 0);

            //TODO: Consider Replacing this Reflection with Source Generator(s) and Analyzer(s)

            //TODO: Error for multiple entry points
            //TODO: Error for no entry points
            //TODO: Error for invalid method parameters
            //TODO: Error for invalid method return type
            //TODO: Error for ActionModule has no parameterless constructor

            var actionMethod = methods.First();

            var type = actionMethod.DeclaringType;

            if (type == null)
            {
                errors.Add(new ErrorViewModel("Failed to get DeclaringType."));

                return dungeonBot with { Errors = errors.ToImmutableList(), ActionModuleContext = null };
            }

            var actionModule = Activator.CreateInstance(type);

            return dungeonBot with { ActionModuleContext = new ActionModuleContext(actionModule, actionMethod) };
        }
    }
}
