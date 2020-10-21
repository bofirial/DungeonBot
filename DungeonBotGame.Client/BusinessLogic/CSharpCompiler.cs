﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace DungeonBotGame.Client.BusinessLogic
{
    public class CSharpCompiler : ICSharpCompiler
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        private const string AbilityExtensionMethodsCode = @"using DungeonBotGame;
using DungeonBotGame.Models.Combat;
namespace DungeonBotGame
{
    public static class ActionComponentExtensionMethods
    {
        public static ITargettedAbilityAction UseHeavySwing(this IActionComponent actionComponent, ITarget target) => ((ActionComponent)actionComponent).UseTargettedAbility(target, AbilityType.HeavySwing);

        public static bool HeavySwingIsAvailable(this IActionComponent actionComponent) => ((ActionComponent)actionComponent).AbilityIsAvailable(AbilityType.HeavySwing);
    }
}";

        private List<MetadataReference>? References { get; set; }

        public CSharpCompiler(HttpClient httpClient, NavigationManager  navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
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
                    CSharpSyntaxTree.ParseText(AbilityExtensionMethodsCode, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview)),
                    CSharpSyntaxTree.ParseText(code, CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.Preview))
                },
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
                    "System.Threading.Tasks",
                    "DungeonBotGame"
                })
            );
        }

        private async Task LoadReferencesAsync()
        {
            References = new List<MetadataReference>();

            var targetReferences = new string[]
            {
                //"Blazor.Extensions.Logging.dll",
                //"BlazorMonaco.dll",
                //"DungeonBotGame.Client.dll",
                "DungeonBotGame.dll",
                "DungeonBotGame.Models.dll",
                //"Fluxor.Blazor.Web.dll",
                //"Fluxor.Blazor.Web.ReduxDevTools.dll",
                //"Fluxor.dll",
                //"Humanizer.dll",
                //"Microsoft.AspNetCore.Authorization.dll",
                //"Microsoft.AspNetCore.Components.dll",
                //"Microsoft.AspNetCore.Components.Forms.dll",
                //"Microsoft.AspNetCore.Components.Web.dll",
                //"Microsoft.AspNetCore.Components.WebAssembly.dll",
                //"Microsoft.AspNetCore.Metadata.dll",
                //"Microsoft.Bcl.AsyncInterfaces.dll",
                //"Microsoft.CodeAnalysis.AnalyzerUtilities.dll",
                //"Microsoft.CodeAnalysis.CSharp.dll",
                //"Microsoft.CodeAnalysis.CSharp.Features.dll",
                //"Microsoft.CodeAnalysis.CSharp.Scripting.dll",
                //"Microsoft.CodeAnalysis.CSharp.Workspaces.dll",
                //"Microsoft.CodeAnalysis.dll",
                //"Microsoft.CodeAnalysis.Features.dll",
                //"Microsoft.CodeAnalysis.Scripting.dll",
                //"Microsoft.CodeAnalysis.Workspaces.dll",
                //"Microsoft.CSharp.dll",
                //"Microsoft.DiaSymReader.dll",
                //"Microsoft.Extensions.Configuration.Abstractions.dll",
                //"Microsoft.Extensions.Configuration.Binder.dll",
                //"Microsoft.Extensions.Configuration.dll",
                //"Microsoft.Extensions.Configuration.FileExtensions.dll",
                //"Microsoft.Extensions.Configuration.Json.dll",
                //"Microsoft.Extensions.DependencyInjection.Abstractions.dll",
                //"Microsoft.Extensions.DependencyInjection.dll",
                //"Microsoft.Extensions.FileProviders.Abstractions.dll",
                //"Microsoft.Extensions.FileProviders.Physical.dll",
                //"Microsoft.Extensions.FileSystemGlobbing.dll",
                //"Microsoft.Extensions.Logging.Abstractions.dll",
                //"Microsoft.Extensions.Logging.dll",
                //"Microsoft.Extensions.Options.dll",
                //"Microsoft.Extensions.Primitives.dll",
                //"Microsoft.JSInterop.dll",
                //"Microsoft.JSInterop.WebAssembly.dll",
                //"Microsoft.VisualBasic.Core.dll",
                //"Microsoft.VisualBasic.dll",
                //"Microsoft.Win32.Primitives.dll",
                //"Microsoft.Win32.Registry.dll",
                "mscorlib.dll",
                "netstandard.dll",
                //"Newtonsoft.Json.dll",
                //"System.AppContext.dll",
                "System.Buffers.dll",
                "System.Collections.Concurrent.dll",
                "System.Collections.dll",
                "System.Collections.Immutable.dll",
                "System.Collections.NonGeneric.dll",
                "System.Collections.Specialized.dll",
                //"System.ComponentModel.Annotations.dll",
                //"System.ComponentModel.DataAnnotations.dll",
                //"System.ComponentModel.dll",
                //"System.ComponentModel.EventBasedAsync.dll",
                //"System.ComponentModel.Primitives.dll",
                //"System.ComponentModel.TypeConverter.dll",
                //"System.Composition.AttributedModel.dll",
                //"System.Composition.Convention.dll",
                //"System.Composition.Hosting.dll",
                //"System.Composition.Runtime.dll",
                //"System.Composition.TypedParts.dll",
                //"System.Configuration.dll",
                "System.Console.dll",
                "System.Core.dll",
                //"System.Data.Common.dll",
                //"System.Data.DataSetExtensions.dll",
                //"System.Data.dll",
                //"System.Diagnostics.Contracts.dll",
                //"System.Diagnostics.Debug.dll",
                //"System.Diagnostics.DiagnosticSource.dll",
                //"System.Diagnostics.FileVersionInfo.dll",
                //"System.Diagnostics.Process.dll",
                //"System.Diagnostics.StackTrace.dll",
                //"System.Diagnostics.TextWriterTraceListener.dll",
                //"System.Diagnostics.Tools.dll",
                //"System.Diagnostics.TraceSource.dll",
                //"System.Diagnostics.Tracing.dll",
                "System.dll",
                //"System.Drawing.dll",
                //"System.Drawing.Primitives.dll",
                //"System.Dynamic.Runtime.dll",
                //"System.Formats.Asn1.dll",
                //"System.Globalization.Calendars.dll",
                //"System.Globalization.dll",
                //"System.Globalization.Extensions.dll",
                //"System.IO.Compression.Brotli.dll",
                //"System.IO.Compression.dll",
                //"System.IO.Compression.FileSystem.dll",
                //"System.IO.Compression.ZipFile.dll",
                //"System.IO.dll",
                //"System.IO.FileSystem.AccessControl.dll",
                //"System.IO.FileSystem.dll",
                //"System.IO.FileSystem.DriveInfo.dll",
                //"System.IO.FileSystem.Primitives.dll",
                //"System.IO.FileSystem.Watcher.dll",
                //"System.IO.IsolatedStorage.dll",
                //"System.IO.MemoryMappedFiles.dll",
                //"System.IO.Pipelines.dll",
                //"System.IO.Pipes.AccessControl.dll",
                //"System.IO.Pipes.dll",
                //"System.IO.UnmanagedMemoryStream.dll",
                "System.Linq.dll",
                "System.Linq.Expressions.dll",
                "System.Linq.Parallel.dll",
                "System.Linq.Queryable.dll",
                "System.Memory.dll",
                "System.Net.dll",
                "System.Net.Http.dll",
                //"System.Net.Http.Json.dll",
                //"System.Net.HttpListener.dll",
                //"System.Net.Mail.dll",
                //"System.Net.NameResolution.dll",
                //"System.Net.NetworkInformation.dll",
                //"System.Net.Ping.dll",
                //"System.Net.Primitives.dll",
                //"System.Net.Requests.dll",
                //"System.Net.Security.dll",
                //"System.Net.ServicePoint.dll",
                //"System.Net.Sockets.dll",
                //"System.Net.WebClient.dll",
                //"System.Net.WebHeaderCollection.dll",
                //"System.Net.WebProxy.dll",
                //"System.Net.WebSockets.Client.dll",
                //"System.Net.WebSockets.dll",
                "System.Numerics.dll",
                "System.Numerics.Vectors.dll",
                "System.ObjectModel.dll",
                "System.Private.CoreLib.dll",
                "System.Private.DataContractSerialization.dll",
                "System.Private.Runtime.InteropServices.JavaScript.dll",
                "System.Private.Uri.dll",
                "System.Private.Xml.dll",
                "System.Private.Xml.Linq.dll",
                "System.Reflection.DispatchProxy.dll",
                "System.Reflection.dll",
                "System.Reflection.Emit.dll",
                "System.Reflection.Emit.ILGeneration.dll",
                "System.Reflection.Emit.Lightweight.dll",
                "System.Reflection.Extensions.dll",
                "System.Reflection.Metadata.dll",
                "System.Reflection.Primitives.dll",
                "System.Reflection.TypeExtensions.dll",
                //"System.Resources.Reader.dll",
                //"System.Resources.ResourceManager.dll",
                //"System.Resources.Writer.dll",
                //"System.Runtime.CompilerServices.Unsafe.dll",
                //"System.Runtime.CompilerServices.VisualC.dll",
                "System.Runtime.dll",
                "System.Runtime.Extensions.dll",
                //"System.Runtime.Handles.dll",
                //"System.Runtime.InteropServices.dll",
                //"System.Runtime.InteropServices.RuntimeInformation.dll",
                //"System.Runtime.Intrinsics.dll",
                //"System.Runtime.Loader.dll",
                //"System.Runtime.Numerics.dll",
                //"System.Runtime.Serialization.dll",
                //"System.Runtime.Serialization.Formatters.dll",
                //"System.Runtime.Serialization.Json.dll",
                //"System.Runtime.Serialization.Primitives.dll",
                //"System.Runtime.Serialization.Xml.dll",
                //"System.Security.AccessControl.dll",
                //"System.Security.Claims.dll",
                //"System.Security.Cryptography.Algorithms.dll",
                //"System.Security.Cryptography.Cng.dll",
                //"System.Security.Cryptography.Csp.dll",
                //"System.Security.Cryptography.Encoding.dll",
                //"System.Security.Cryptography.OpenSsl.dll",
                //"System.Security.Cryptography.Primitives.dll",
                //"System.Security.Cryptography.X509Certificates.dll",
                //"System.Security.dll",
                //"System.Security.Principal.dll",
                //"System.Security.Principal.Windows.dll",
                //"System.Security.SecureString.dll",
                //"System.ServiceModel.Web.dll",
                //"System.ServiceProcess.dll",
                //"System.Text.Encoding.CodePages.dll",
                //"System.Text.Encoding.dll",
                //"System.Text.Encoding.Extensions.dll",
                //"System.Text.Encodings.Web.dll",
                "System.Text.Json.dll",
                //"System.Text.RegularExpressions.dll",
                //"System.Threading.Channels.dll",
                //"System.Threading.dll",
                //"System.Threading.Overlapped.dll",
                //"System.Threading.Tasks.Dataflow.dll",
                //"System.Threading.Tasks.dll",
                "System.Threading.Tasks.Extensions.dll",
                //"System.Threading.Tasks.Parallel.dll",
                //"System.Threading.Thread.dll",
                //"System.Threading.ThreadPool.dll",
                //"System.Threading.Timer.dll",
                //"System.Transactions.dll",
                //"System.Transactions.Local.dll",
                "System.ValueTuple.dll",
                //"System.Web.dll",
                //"System.Web.HttpUtility.dll",
                //"System.Windows.dll",
                //"System.Xml.dll",
                //"System.Xml.Linq.dll",
                //"System.Xml.ReaderWriter.dll",
                //"System.Xml.Serialization.dll",
                //"System.Xml.XDocument.dll",
                //"System.Xml.XmlDocument.dll",
                //"System.Xml.XmlSerializer.dll",
                //"System.Xml.XPath.dll",
                //"System.Xml.XPath.XDocument.dll",
                //"WindowsBase.dll",
            };

            foreach (var reference in targetReferences)
            {
                var stream = await _httpClient.GetStreamAsync(new Uri($"{_navigationManager.BaseUri}_framework/{reference}"));
                References.Add(MetadataReference.CreateFromStream(stream));
            }
        }
    }
}
