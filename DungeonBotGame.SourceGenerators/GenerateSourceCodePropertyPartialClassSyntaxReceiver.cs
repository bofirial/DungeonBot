using System.Collections.Generic;
//using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DungeonBotGame.SourceGenerators
{
    public class GenerateSourceCodePropertyPartialClassSyntaxReceiver : ISyntaxReceiver
    {
        public List<ClassDeclarationSyntax> ClassesToAugment { get; private set; } = new List<ClassDeclarationSyntax>();

        public void OnVisitSyntaxNode(SyntaxNode syntaxNode)
        {
            //var syntaxGenerator = SyntaxGenerator.GetGenerator()

            if (syntaxNode is ClassDeclarationSyntax cds &&
                cds.Identifier.Text == "WolfKingActionModule")
                //cds.AttributeLists.Any(al => al.Attributes.Any(a => a.)))
            {
                System.Diagnostics.Debugger.Launch();
                ClassesToAugment.Add(cds);
            }
        }
    }
}
