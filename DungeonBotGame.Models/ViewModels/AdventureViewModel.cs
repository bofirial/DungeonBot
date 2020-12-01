using System.Collections.Immutable;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureViewModel(string Id, string Name, string Description, IImmutableList<EncounterViewModel> Encounters, int DungeonBotSlots, string Status, IImmutableList<AdventureResultViewModel> AdventureResults);
}
