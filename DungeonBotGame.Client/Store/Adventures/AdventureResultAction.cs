using DungeonBotGame.Models.ViewModels;

namespace DungeonBotGame.Client.Store.Adventures
{
    public class AdventureResultAction
    {
        public AdventureViewModel Adventure { get; }
        public AdventureResultViewModel AdventureResult { get; }

        public AdventureResultAction(AdventureViewModel adventure, AdventureResultViewModel adventureResult)
        {
            Adventure = adventure;
            AdventureResult = adventureResult;
        }
    }
}
