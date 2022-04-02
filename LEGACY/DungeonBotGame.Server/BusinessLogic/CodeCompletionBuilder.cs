using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using DungeonBotGame.Models.Api;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using CompletionItem = DungeonBotGame.Models.Api.CompletionItem;

namespace DungeonBotGame.Server.BusinessLogic
{
    public interface ICodeCompletionBuilder
    {
        Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(CodeCompletionPostRequestModel requestModel);
    }

    public class CodeCompletionBuilder : ICodeCompletionBuilder
    {
        private readonly IActionComponentAbilityExtensionMethodsClassBuilder _actionComponentAbilityExtensionMethodsClassBuilder;

        public CodeCompletionBuilder(IActionComponentAbilityExtensionMethodsClassBuilder actionComponentAbilityExtensionMethodsClassBuilder)
        {
            _actionComponentAbilityExtensionMethodsClassBuilder = actionComponentAbilityExtensionMethodsClassBuilder;
        }

        public async Task<CodeCompletionPostResponseModel> GetCodeCompletionsAsync(CodeCompletionPostRequestModel requestModel)
        {
            var completionResults = await BuildCompletionServiceAndGetCompletions(requestModel);

            return BuildCodeCompletionPostResponseModel(completionResults);
        }

        private async Task<CompletionList> BuildCompletionServiceAndGetCompletions(CodeCompletionPostRequestModel requestModel)
        {
            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            using var workspace = new AdhocWorkspace(host);

            var sourceCodeFile = requestModel.DungeonBot.ActionModuleFiles.First(s => s.FileName == requestModel.TargetFileName);

            var metadataReferences = new MetadataReference[] {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ActionModuleEntrypointAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(GetSystemRuntimeAssemblyName()).Location)
            };

            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "DungeonBotGame", "DungeonBotGame", LanguageNames.CSharp)
                .WithMetadataReferences(metadataReferences);

            var project = workspace.AddProject(projectInfo);
            workspace.AddDocument(project.Id, "AbilityExtensionMethods.cs", SourceText.From(_actionComponentAbilityExtensionMethodsClassBuilder.BuildAbilityExtensionMethodsClass(requestModel.DungeonBot)));
            var document = workspace.AddDocument(project.Id, sourceCodeFile.FileName, SourceText.From(sourceCodeFile.Content));

            var completionService = CompletionService.GetService(document);

            return await completionService.GetCompletionsAsync(document, requestModel.TargetFilePosition);
        }

        private static string GetSystemRuntimeAssemblyName()
        {
            var entryAssembly = Assembly.GetEntryAssembly();

            if (entryAssembly == null)
            {
                throw new ApplicationException("How is there no entry assembly?");
            }

            return entryAssembly.GetReferencedAssemblies().First(a => a?.Name?.Contains("System.Runtime") == true).FullName;
        }

        private static CodeCompletionPostResponseModel BuildCodeCompletionPostResponseModel(CompletionList completionResults)
        {
            if (completionResults == null)
            {
                return new CodeCompletionPostResponseModel(ImmutableList.Create<CompletionItem>(), "No Completions Found");
            }

            return new CodeCompletionPostResponseModel(completionResults.Items.Select(i =>
            {
                if (i.Properties.ContainsKey("SymbolName"))
                {
                    return new CompletionItem(i.Properties["SymbolName"], i.Properties["SymbolName"], i.Properties["SymbolKind"], i.Properties["SymbolName"]);
                }
                else
                {
                    return new CompletionItem(i.DisplayText, i.DisplayText, "9", i.DisplayText); ;
                }
            }).ToImmutableList(), null);
        }
    }
}
