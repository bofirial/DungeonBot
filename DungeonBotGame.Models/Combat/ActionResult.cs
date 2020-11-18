using System.Collections.Immutable;

namespace DungeonBotGame.Models.Combat
{
    public record ActionResult(
        int CombatTime,
        ICharacter Character,
        string DisplayText,
        IAction Action,
        IImmutableList<CharacterRecord> Characters,
        IImmutableList<CombatEvent> NewCombatEvents) : IActionResult;
}
