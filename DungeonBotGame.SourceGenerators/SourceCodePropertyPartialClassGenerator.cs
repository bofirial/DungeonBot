using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace DungeonBotGame.SourceGenerators
{
    [Generator]
    public class SourceCodePropertyPartialClassGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var targetAttributeType = context.Compilation.References
                                    .Select(context.Compilation.GetAssemblyOrModuleSymbol)
                                    .OfType<IAssemblySymbol>()
                                    .Select(assemblySymbol => assemblySymbol.GetTypeByMetadataName("DungeonBotGame.SourceGenerators.Attributes.GenerateSourceCodePropertyPartialClassAttribute"))
                                    .FirstOrDefault(t => t != null);


            foreach (var syntaxTree in context.Compilation.SyntaxTrees)
            {
                var semanticModel = context.Compilation.GetSemanticModel(syntaxTree);

                var syntaxRoot = syntaxTree.GetRoot();
                var attributes = syntaxRoot.DescendantNodes().OfType<AttributeSyntax>();

                foreach (var attribute in attributes)
                {
                    if (SymbolEqualityComparer.Default.Equals(semanticModel.GetTypeInfo(attribute).Type, targetAttributeType) &&
                        attribute.Parent.Parent is ClassDeclarationSyntax classDeclaration)
                    {

                        var className = classDeclaration.Identifier.ValueText;

                        var i = 0;
                        SyntaxNode compilationUnit = classDeclaration;

                        while (i < 30 && compilationUnit.Parent != null)
                        {
                            compilationUnit = compilationUnit.Parent;
                        }

                        var sourceCode = compilationUnit.ToString().Replace("\"", "\"\"");

                        var sourceText = SourceText.From(@$"namespace DungeonBotGame.Client.BusinessLogic.EnemyActionModules
                        {{
                            public partial class { className }
                            {{
                                public string SourceCode {{ get; set; }} = @""{ sourceCode }"";
                            }}
                        }}", Encoding.UTF8);

                        context.AddSource($"{className}.Generated.cs", sourceText);

                        //System.Diagnostics.Debugger.Launch();
                    }
                }
            }
        }
        public void Initialize(GeneratorInitializationContext context)
        {
            //context.RegisterForSyntaxNotifications(() => new GenerateSourceCodePropertyPartialClassSyntaxReceiver());
        }
    }
}
