using Microsoft.CodeAnalysis;

namespace DungeonBotGame.SourceGenerators;

[Generator]
public class DungeonBotPartialClassGenerator : ISourceGenerator
{
    private const string DungeonBotAttributeMetadataName = "DungeonBot.Foundation.DungeonBotAttribute";

    public void Execute(GeneratorExecutionContext context)
    {
        var targetAttributeType = context.Compilation.References
            .Select(context.Compilation.GetAssemblyOrModuleSymbol)
            .OfType<IAssemblySymbol>()
            .Select(assemblySymbol => assemblySymbol.GetTypeByMetadataName(DungeonBotAttributeMetadataName))
            .FirstOrDefault(t => t != null);

        if (context.SyntaxReceiver is GenerateDungeonBotPartialClassSyntaxReceiver generateDungeonBotPartialClassSyntaxReceiver)
        {
            foreach (var (classDeclarationSyntax, attributeSyntax) in generateDungeonBotPartialClassSyntaxReceiver.ClassesToAugment)
            {
                var semanticModel = context.Compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);

                if (SymbolEqualityComparer.Default.Equals(semanticModel.GetTypeInfo(attributeSyntax).Type, targetAttributeType))
                {
                    //                    var className = classDeclarationSyntax.Identifier.ValueText;

                    //                    var i = 0;
                    //                    SyntaxNode compilationUnit = classDeclarationSyntax;

                    //                    while (i < 30 && compilationUnit.Parent != null)
                    //                    {
                    //                        compilationUnit = compilationUnit.Parent;
                    //                    }
                    //                    if (i >= 30)
                    //                    {
                    //                        throw new Exception("Could not find ActionModule Class");
                    //                    }

                    //                    var sourceCode = compilationUnit.ToFullString().Replace("\"", "\"\"");

                    //                    var sourceText = SourceText.From(@$"using System.Collections.Immutable;
                    //using DungeonBotGame.Models.ViewModels;

                    //namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
                    //{{
                    //    public partial class { className }
                    //    {{
                    //        public IImmutableList<ActionModuleFileViewModel> SourceCodeFiles {{ get; }} = ImmutableList.Create(new ActionModuleFileViewModel(""EnemyActionModule001.cs"", @""{ sourceCode }""));
                    //    }}
                    //}}", Encoding.UTF8);

                    //                    context.AddSource($"{className}.Generated.cs", sourceText);
                }
            }
        }
    }

    public void Initialize(GeneratorInitializationContext context) => context.RegisterForSyntaxNotifications(() => new GenerateDungeonBotPartialClassSyntaxReceiver());
}
