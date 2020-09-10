using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DungeonBot.Client.BusinessLogic
{
    public class CSharpCompiler : ICSharpCompiler
    {
        private readonly HttpClient _httpClient;

        private List<MetadataReference>? References { get; set; }

        public CSharpCompiler(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CSharpCompilation> CompileAsync(string code)
        {
            if (References == null)
            {
                await LoadReferencesAsync();
            }

            return CSharpCompilation.Create(
                Path.GetRandomFileName(),
                new List<SyntaxTree>() {
                CSharpSyntaxTree.ParseText(code, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview))},
                References,
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
                    "System.Threading.Tasks"
                })
            );
        }

        private async Task LoadReferencesAsync()
        {
            var refs = AppDomain.CurrentDomain.GetAssemblies();

            References = new List<MetadataReference>();

            foreach (var reference in refs.Where(x => !x.IsDynamic && !string.IsNullOrWhiteSpace(x.Location)))
            {
                var stream = await _httpClient.GetStreamAsync($"_framework/_bin/{reference.Location}");
                References.Add(MetadataReference.CreateFromStream(stream));
            }
        }
    }
}
