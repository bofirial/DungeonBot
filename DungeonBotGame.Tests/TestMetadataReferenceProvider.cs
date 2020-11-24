using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using DungeonBotGame.Models.ViewModels;
using Microsoft.CodeAnalysis;

namespace DungeonBotGame.Tests
{
    public class TestMetadataReferenceProvider : IMetadataReferenceProvider
    {
        public Task<List<MetadataReference>> LoadReferencesAsync()
        {
            return Task.FromResult(new List<MetadataReference> {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ActionModuleEntrypointAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(DungeonBotViewModel).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(HttpClient).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Expression).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(GetSystemRuntimeAssemblyName()).Location)
            });
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
    }
}
