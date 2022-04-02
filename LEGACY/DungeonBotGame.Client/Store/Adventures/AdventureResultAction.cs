using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record AdventureResultAction(AdventureViewModel Adventure, AdventureResultViewModel AdventureResult);
}
