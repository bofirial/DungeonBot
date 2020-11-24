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
        public async Task<List<MetadataReference>> LoadReferencesAsync()
        {
            //var metadataReferences = new List<MetadataReference>();

            //var targetReferences = new string[]
            //{
            //    "DungeonBotGame.dll",
            //    "DungeonBotGame.Models.dll",
            //    "mscorlib.dll",
            //    "netstandard.dll",
            //    "Newtonsoft.Json.dll",
            //    "System.Buffers.dll",
            //    "System.Collections.Concurrent.dll",
            //    "System.Collections.dll",
            //    "System.Collections.Immutable.dll",
            //    "System.Collections.NonGeneric.dll",
            //    "System.Collections.Specialized.dll",
            //    "System.Console.dll",
            //    "System.Core.dll",
            //    "System.dll",
            //    "System.Linq.dll",
            //    "System.Linq.Expressions.dll",
            //    "System.Linq.Parallel.dll",
            //    "System.Linq.Queryable.dll",
            //    "System.Memory.dll",
            //    "System.Net.dll",
            //    "System.Net.Http.dll",
            //    "System.Numerics.dll",
            //    "System.Numerics.Vectors.dll",
            //    "System.ObjectModel.dll",
            //    "System.Private.CoreLib.dll",
            //    "System.Private.DataContractSerialization.dll",
            //    "System.Private.Runtime.InteropServices.JavaScript.dll",
            //    "System.Private.Uri.dll",
            //    "System.Private.Xml.dll",
            //    "System.Private.Xml.Linq.dll",
            //    "System.Reflection.DispatchProxy.dll",
            //    "System.Reflection.dll",
            //    "System.Reflection.Emit.dll",
            //    "System.Reflection.Emit.ILGeneration.dll",
            //    "System.Reflection.Emit.Lightweight.dll",
            //    "System.Reflection.Extensions.dll",
            //    "System.Reflection.Metadata.dll",
            //    "System.Reflection.Primitives.dll",
            //    "System.Reflection.TypeExtensions.dll",
            //    "System.Runtime.dll",
            //    "System.Runtime.Extensions.dll",
            //    "System.Threading.Tasks.Extensions.dll",
            //    "System.ValueTuple.dll",
            //};

            //foreach (var reference in targetReferences)
            //{

            //}

            return new List<MetadataReference> {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(ActionModuleEntrypointAttribute).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(DungeonBotViewModel).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(HttpClient).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(Expression).Assembly.Location),
                MetadataReference.CreateFromFile(Assembly.Load(GetSystemRuntimeAssemblyName()).Location)
            };
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
