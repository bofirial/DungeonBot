using System.Collections.Immutable;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace DungeonBotGame.SourceGenerators;

[Generator(LanguageNames.CSharp)]
public class DungeonBotPartialClassGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        // Add the DungeonBot marker attribute to the compilation
        context.RegisterPostInitializationOutput(ctx => ctx.AddSource(
            "DungeonBotAttribute.g.cs",
            SourceText.From(SourceGenerationHelper.DungeonBotAttribute, Encoding.UTF8)));

        var textFiles = context.AdditionalTextsProvider.Where(static file => file.Path.EndsWith("gameState.json"));

        // Do a simple filter for classes
        var classDeclarations = context.SyntaxProvider
            .CreateSyntaxProvider(
                predicate: static (s, _) => IsSyntaxTargetForGeneration(s), // select classes with attributes
                transform: static (ctx, _) => GetSemanticTargetForGeneration(ctx)) // select the class with the [DungeonBot] attribute
            .Where(static m => m is not null)!; // filter out attributed classes that we don't care about

        // Combine the selected classes with the `Compilation`
        var compilationAndClasses = context.CompilationProvider
            .Combine(classDeclarations.Collect())
            .Combine(textFiles.Collect());

        // Generate the source using the compilation and classes
        context.RegisterSourceOutput(compilationAndClasses,
            static (spc, source) => Execute(source.Left.Left, source.Left.Right, source.Right, spc));
    }

    private static bool IsSyntaxTargetForGeneration(SyntaxNode node) => node is ClassDeclarationSyntax m && m.AttributeLists.Count > 0;

    private const string DungeonBotAttribute = "DungeonBotGame.Foundation.DungeonBotAttribute";

    private static ClassDeclarationSyntax? GetSemanticTargetForGeneration(GeneratorSyntaxContext context)
    {
        // we know the node is a ClassDeclarationSyntax thanks to IsSyntaxTargetForGeneration
        var classDeclarationSyntax = (ClassDeclarationSyntax)context.Node;

        // loop through all the attributes on the method
        foreach (var attributeListSyntax in classDeclarationSyntax.AttributeLists)
        {
            foreach (var attributeSyntax in attributeListSyntax.Attributes)
            {
                if (context.SemanticModel.GetSymbolInfo(attributeSyntax).Symbol is not IMethodSymbol attributeSymbol)
                {
                    // weird, we couldn't get the symbol, ignore it
                    continue;
                }

                var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
                var fullName = attributeContainingTypeSymbol.ToDisplayString();

                // Is the attribute the [DungeonBot] attribute?
                if (fullName == DungeonBotAttribute)
                {
                    // return the class
                    return classDeclarationSyntax;
                }
            }
        }

        // we didn't find the attribute we were looking for
        return null;
    }

    private static void Execute(Compilation compilation, ImmutableArray<ClassDeclarationSyntax?> classes, ImmutableArray<AdditionalText> gameStateJsonFiles, SourceProductionContext context)
    {
        if (classes.IsDefaultOrEmpty || gameStateJsonFiles.IsDefaultOrEmpty || gameStateJsonFiles.Count() > 1)
        {
            // nothing to do yet
            return;
        }

        var distinctClasses = classes.Distinct();

        var gameStateJsonFile = gameStateJsonFiles.First();

        // Convert each ClassDeclarationSyntax to a DungeonBotPartialClassToGenerate
        var dungeonBotPartialClassesToGenerate = GetTypesToGenerate(compilation, distinctClasses, context.CancellationToken);

        // If there were errors in the ClassDeclarationSyntax, we won't create a ClassToGenerate for it, so make sure we have something to generate
        if (dungeonBotPartialClassesToGenerate.Count > 0)
        {
            foreach (var dungeonBotPartialClassToGenerate in dungeonBotPartialClassesToGenerate)
            {
                // generate the source code and add it to the output
                var result = SourceGenerationHelper.GenerateDungeonBotPartialClass(dungeonBotPartialClassToGenerate);
                context.AddSource($"{dungeonBotPartialClassToGenerate.Name}.g.cs", SourceText.From(result, Encoding.UTF8));
            }
        }
    }

    private static List<DungeonBotPartialClassToGenerate> GetTypesToGenerate(Compilation compilation, IEnumerable<ClassDeclarationSyntax?> classes, CancellationToken ct)
    {
        // Create a list to hold our output
        var dungeonBotPartialClassesToGenerates = new List<DungeonBotPartialClassToGenerate>();
        // Get the semantic representation of our marker attribute
        var dungeonBotAttribute = compilation.GetTypeByMetadataName(DungeonBotAttribute);

        if (dungeonBotAttribute == null)
        {
            // If this is null, the compilation couldn't find the marker attribute type
            // which suggests there's something very wrong! Bail out..
            return dungeonBotPartialClassesToGenerates;
        }

        foreach (var classDeclarationSyntax in classes)
        {
            // stop if we're asked to
            ct.ThrowIfCancellationRequested();

            if (classDeclarationSyntax == null)
            {
                continue;
            }

            // Get the semantic representation of the class syntax
            var semanticModel = compilation.GetSemanticModel(classDeclarationSyntax.SyntaxTree);
            if (semanticModel.GetDeclaredSymbol(classDeclarationSyntax) is not INamedTypeSymbol classSymbol)
            {
                // something went wrong, bail out
                continue;
            }

            var className = classDeclarationSyntax.Identifier.ValueText;

            var dungeonBotNames = new List<string>();

            foreach (var attributeData in classSymbol.GetAttributes())
            {
                if (!dungeonBotAttribute.Equals(attributeData.AttributeClass, SymbolEqualityComparer.Default))
                {
                    // This isn't the [DungeonBot] attribute
                    continue;
                }

                // This is the right attribute, check the constructor arguments
                if (!attributeData.ConstructorArguments.IsEmpty)
                {
                    var args = attributeData.ConstructorArguments;

                    // make sure we don't have any errors
                    foreach (var arg in args)
                    {
                        if (arg.Kind == TypedConstantKind.Error)
                        {
                            // have an error, so don't try and do any generation
                            return new List<DungeonBotPartialClassToGenerate>();
                        }
                    }

                    // Use the position of the argument to infer which value is set
                    switch (args.Length)
                    {
                        case 1:
                            var typedConstant = args[0];

                            if (typedConstant.Value != null)
                            {
                                var dungeonBotName = (string)typedConstant.Value;

                                if (dungeonBotName != null && !dungeonBotNames.Contains(dungeonBotName))
                                {
                                    dungeonBotNames.Add(dungeonBotName);
                                }
                            }

                            break;
                    }
                }

                // This is the attribute, check all of the named arguments
                if (!attributeData.NamedArguments.IsEmpty)
                {
                    foreach (var namedArgument in attributeData.NamedArguments)
                    {
                        var typedConstant = namedArgument.Value;

                        if (typedConstant.Kind == TypedConstantKind.Error)
                        {
                            // have an error, so don't try and do any generation
                            return new List<DungeonBotPartialClassToGenerate>();
                        }

                        // Use the constructor argument or property name to infer which value is set
                        switch (namedArgument.Key)
                        {
                            case "Name":
                            case "name":

                                if (typedConstant.Value != null)
                                {
                                    var dungeonBotName = (string)typedConstant.Value;

                                    if (dungeonBotName != null && !dungeonBotNames.Contains(dungeonBotName))
                                    {
                                        dungeonBotNames.Add(dungeonBotName);
                                    }
                                }

                                break;
                        }
                    }
                }
            }

            // Create a DungeonBotPartialClassToGenerate for use in the generation phase
            dungeonBotPartialClassesToGenerates.Add(new DungeonBotPartialClassToGenerate(className, GetNamespace(classDeclarationSyntax), dungeonBotNames));
        }

        return dungeonBotPartialClassesToGenerates;
    }

    // determine the namespace the class/enum/struct is declared in, if any
    private static string GetNamespace(BaseTypeDeclarationSyntax syntax)
    {
        // If we don't have a namespace at all we'll return an empty string
        // This accounts for the "default namespace" case
        var nameSpace = string.Empty;

        // Get the containing syntax node for the type declaration
        // (could be a nested type, for example)
        var potentialNamespaceParent = syntax.Parent;

        // Keep moving "out" of nested classes etc until we get to a namespace
        // or until we run out of parents
        while (potentialNamespaceParent != null &&
                potentialNamespaceParent is not NamespaceDeclarationSyntax
                && potentialNamespaceParent is not FileScopedNamespaceDeclarationSyntax)
        {
            potentialNamespaceParent = potentialNamespaceParent.Parent;
        }

        // Build up the final namespace by looping until we no longer have a namespace declaration
        if (potentialNamespaceParent is BaseNamespaceDeclarationSyntax namespaceParent)
        {
            // We have a namespace. Use that as the type
            nameSpace = namespaceParent.Name.ToString();

            // Keep moving "out" of the namespace declarations until we
            // run out of nested namespace declarations
            while (true)
            {
                if (namespaceParent.Parent is not NamespaceDeclarationSyntax parent)
                {
                    break;
                }

                // Add the outer namespace as a prefix to the final namespace
                nameSpace = $"{namespaceParent.Name}.{nameSpace}";
                namespaceParent = parent;
            }
        }

        // return the final namespace
        return nameSpace;
    }
}
