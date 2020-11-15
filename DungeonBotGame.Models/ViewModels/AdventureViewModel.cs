using System.Collections.Immutable;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureViewModel(string Name, string Description, IImmutableList<EncounterViewModel> Encounters, string Status, IImmutableList<AdventureResultViewModel> AdventureResults);
}
