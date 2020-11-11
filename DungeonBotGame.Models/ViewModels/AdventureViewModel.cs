using System.Collections.Generic;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureViewModel(string Name, string Description, IReadOnlyCollection<EncounterViewModel> Encounters, string Status, IReadOnlyCollection<AdventureResultViewModel> AdventureResults);
}
