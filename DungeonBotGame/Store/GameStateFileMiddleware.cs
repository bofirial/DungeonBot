using System.Collections.Immutable;
using System.Text.Json;
using System.Text.Json.Serialization;
using DungeonBotGame.Data;
using Fluxor;
using Microsoft.AspNetCore.Hosting;

namespace DungeonBotGame.Store;
public class GameStateFileMiddleware : Middleware
{
    private readonly JsonSerializerOptions _jsonSerializerOptions = new()
    {
        Converters = { new JsonStringEnumConverter() },
        WriteIndented = true
    };
    private readonly IWebHostEnvironment _webHostEnvironment;

    public GameStateFileMiddleware(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public override async Task InitializeAsync(IDispatcher dispatcher, IStore store)
    {
        var gameState = await LoadGameState();

        var dungeonBotFeature = store.Features.Values.FirstOrDefault(x => x.GetName() == nameof(DungeonBotState));

        if (dungeonBotFeature != null)
        {
            dungeonBotFeature.RestoreState(gameState.DungeonBotState);

            dungeonBotFeature.StateChanged += async (sender, args) =>
            {
                var gameState = await LoadGameState();

                var dungeonBotState = (DungeonBotState)dungeonBotFeature.GetState();

                await SaveGameState(gameState with { DungeonBotState = dungeonBotState });
            };
        }

        await base.InitializeAsync(dispatcher, store);
    }

    private async Task<GameState> LoadGameState()
    {
        var gameStateFileContent = await File.ReadAllTextAsync(GetGameStateFilePath());

        if (string.IsNullOrEmpty(gameStateFileContent))
        {
            //TODO: Should this make the default GameState instead?
            return CreateEmptyGameState();
        }

        var gameState = JsonSerializer.Deserialize<GameState>(gameStateFileContent, _jsonSerializerOptions);

        if (gameState == null)
        {
            //TODO: Should this make the default GameState instead?
            return CreateEmptyGameState();
        }

        return gameState;
    }

    private async Task SaveGameState(GameState gameState)
    {
        var gameStateFileContent = JsonSerializer.Serialize(gameState, _jsonSerializerOptions);

        await File.WriteAllTextAsync(GetGameStateFilePath(), gameStateFileContent);
    }

    private string GetGameStateFilePath() => $"{_webHostEnvironment.ContentRootPath}gameState.json";
    private GameState CreateEmptyGameState() => new GameState(new DungeonBotState(ImmutableList<DungeonBotViewModel>.Empty));
}
