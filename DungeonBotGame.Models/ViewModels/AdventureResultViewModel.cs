using System.Collections.Immutable;

namespace DungeonBotGame.Models.ViewModels
{
    public record AdventureResultViewModel(string RunId, bool Success, string ResultDisplayText, IImmutableList<EncounterResultViewModel> EncounterResults);
}
