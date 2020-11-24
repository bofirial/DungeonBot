using System;
using DungeonBotGame.Client;
using DungeonBotGame.Client.BusinessLogic.Compilation;
using Microsoft.Extensions.DependencyInjection;

namespace DungeonBotGame.Tests
{
    public class DependencyInjectionFixture : IDisposable
    {
        public DependencyInjectionFixture()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddScoped<IMetadataReferenceProvider, TestMetadataReferenceProvider>();

            Program.ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();
        }

        public ServiceProvider ServiceProvider { get; private set; }

        public void Dispose() => ServiceProvider?.Dispose();
    }
}
