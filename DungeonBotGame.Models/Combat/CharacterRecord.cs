using System.Collections.Immutable;

namespace DungeonBotGame.Models.Combat
{
    public record CharacterRecord(string Id, string Name, int MaximumHealth, int CurrentHealth, bool IsDungeonBot, ImmutableList<CombatEffect> CombatEffects) : ICharacter;
}
