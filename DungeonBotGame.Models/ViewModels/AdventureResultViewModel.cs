using System.Collections.ObjectModel;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureResultViewModel(string RunId, bool Success, ReadOnlyCollection<EncounterResultViewModel> EncounterResults);
}
