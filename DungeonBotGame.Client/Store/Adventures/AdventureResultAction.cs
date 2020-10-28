using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public record AdventureResultAction
    {
        public AdventureViewModel Adventure { get; init; }

        public AdventureResultViewModel AdventureResult { get; init; }

        public AdventureResultAction(AdventureViewModel adventure, AdventureResultViewModel adventureResult)
        {
            Adventure = adventure;
            AdventureResult = adventureResult;
        }
    }
}
