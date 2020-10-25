﻿using System.Linq;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic;
using DungeonBotGame.Models.Api;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;
using CompletionItem = DungeonBotGame.Models.Api.CompletionItem;

namespace DungeonBotGame.Server.BusinessLogic
{
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

            var sourceCodeFile = requestModel.ActionModuleLibrary.ActionModuleFiles.First(s => s.FileName == requestModel.TargetFileName);

            var metadataReferences = new MetadataReference[] {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ActionModuleEntrypointAttribute).Assembly.Location)
            };

            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "DungeonBotGame", "DungeonBotGame", LanguageNames.CSharp)
                .WithMetadataReferences(metadataReferences);

            var project = workspace.AddProject(projectInfo);
            workspace.AddDocument(project.Id, "AbilityExtensionMethods.cs", SourceText.From(_actionComponentAbilityExtensionMethodsClassBuilder.BuildAbilityExtensionMethodsClass(requestModel.DungeonBot)));
            var document = workspace.AddDocument(project.Id, sourceCodeFile.FileName, SourceText.From(sourceCodeFile.Content));

            var completionService = CompletionService.GetService(document);

            return await completionService.GetCompletionsAsync(document, requestModel.TargetFilePosition);
        }

        private static CodeCompletionPostResponseModel BuildCodeCompletionPostResponseModel(CompletionList completionResults)
        {
            if (completionResults == null)
            {
                return new CodeCompletionPostResponseModel() { ErrorMessage = "No Completions Found" };
            }

            return new CodeCompletionPostResponseModel()
            {
                CompletionItems = completionResults.Items.Select(i =>
                {
                    if (i.Properties.ContainsKey("SymbolName"))
                    {
                        return new CompletionItem()
                        {
                            Label = i.Properties["SymbolName"],
                            InsertText = i.Properties["SymbolName"],
                            Kind = i.Properties["SymbolKind"],
                            Detail = i.Properties["SymbolName"]
                        };
                    }
                    else
                    {
                        return new CompletionItem()
                        {
                            Label = i.DisplayText,
                            InsertText = i.DisplayText,
                            Kind = "9",
                            Detail = i.DisplayText
                        };
                    }
                }).ToList()
            };
        }
    }
}