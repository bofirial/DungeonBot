using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DungeonBotGame.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DungeonBotGame.Client.BusinessLogic.Compilation
{
    public interface ICSharpCompiler
    {
        Task<CSharpCompilation> CompileAsync(DungeonBotViewModel dungeonBot);
    }

    public class CSharpCompiler : ICSharpCompiler
    {
        private readonly IMetadataReferenceProvider _metadataReferenceProvider;
        private readonly IActionComponentAbilityExtensionMethodsClassBuilder _actionComponentAbilityExtensionMethodsClassBuilder;

        private List<MetadataReference>? _references;

        public CSharpCompiler(IMetadataReferenceProvider metadataReferenceProvider, IActionComponentAbilityExtensionMethodsClassBuilder actionComponentAbilityExtensionMethodsClassBuilder)
        {
            _metadataReferenceProvider = metadataReferenceProvider;
            _actionComponentAbilityExtensionMethodsClassBuilder = actionComponentAbilityExtensionMethodsClassBuilder;
        }

        public async Task<CSharpCompilation> CompileAsync(DungeonBotViewModel dungeonBot)
        {
            if (_references == null)
            {
                _references = await _metadataReferenceProvider.LoadReferencesAsync();
            }

            var syntaxTrees = dungeonBot.ActionModuleFiles.Select(f => CSharpSyntaxTree.ParseText(f.Content, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview))).ToList();
            syntaxTrees.Add(CSharpSyntaxTree.ParseText(
                _actionComponentAbilityExtensionMethodsClassBuilder.BuildAbilityExtensionMethodsClass(dungeonBot),
                CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview)));

            return CSharpCompilation.Create(
                Path.GetRandomFileName(),
                syntaxTrees,
                _references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary, usings: new[]
                {
                    "System",
                    "System.IO",
                    "System.Collections.Generic",
                    "System.Console",
                    "System.Diagnostics",
                    "System.Dynamic",
                    "System.Linq",
                    "System.Linq.Expressions",
                    "System.Net.Http",
                    "System.Text",
                    "System.Threading.Tasks",
                    "DungeonBotGame"
                })
            );
        }
    }
}
