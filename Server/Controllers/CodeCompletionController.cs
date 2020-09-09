using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Completion;
using Microsoft.CodeAnalysis.Host.Mef;
using Microsoft.CodeAnalysis.Text;

namespace DungeonBot.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodeCompletionController : ControllerBase
    {
        public async Task<CompletionList?> GetAsync(string code, int position)
        {
            var host = MefHostServices.Create(MefHostServices.DefaultAssemblies);
            var workspace = new AdhocWorkspace(host);

        //    var code = @"using System;
        //public class MyClass
        //{
        //    public static void MyMethod(int value)
        //    {
        //        Guid.
        //    }
        //}";

            var projectInfo = ProjectInfo.Create(ProjectId.CreateNewId(), VersionStamp.Create(), "DungeonBot", "DungeonBot", LanguageNames.CSharp).
                    WithMetadataReferences(new[] { MetadataReference.CreateFromFile(typeof(object).Assembly.Location) });
            var project = workspace.AddProject(projectInfo);
            var document = workspace.AddDocument(project.Id, "MyDungeonBot.cs", SourceText.From(code));

            var completionService = CompletionService.GetService(document);

            //var codePosition = code.LastIndexOf("Guid.") + 5;
            var results = await completionService.GetCompletionsAsync(document, position);

            return results;
        }
    }
}
