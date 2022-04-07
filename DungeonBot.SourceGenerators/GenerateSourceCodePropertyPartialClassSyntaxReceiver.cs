using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DungeonBotGame.SourceGenerators;
public class GenerateDungeonBotPartialClassSyntaxReceiver : ISyntaxReceiver
{
    public List<(ClassDeclarationSyntax Class, AttributeSyntax Attribute)> ClassesToAugment { get; } = new List<(ClassDeclarationSyntax Class, AttributeSyntax Attribute)>();

    public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
    {
        if (syntaxNode is AttributeSyntax attributeSyntax &&
            syntaxNode?.Parent?.Parent != null &&
            syntaxNode?.Parent?.Parent is ClassDeclarationSyntax classDeclarationSyntax)
        {
            ClassesToAugment.Add((classDeclarationSyntax, attributeSyntax));
        }
    }
}
