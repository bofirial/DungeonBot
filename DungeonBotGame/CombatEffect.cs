using System.Diagnostics.CodeAnalysis;

namespace DungeonBotGame
{
    public record CombatEffect(string Name, CombatEffectType CombatEffectType, short Value);

    [SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "It is used.  Not sure why this is flagged here.")]
    public record CombatEffect<TCombatEffectData>(string Name, CombatEffectType CombatEffectType, short Value, TCombatEffectData CombatEffectData) : CombatEffect(Name, CombatEffectType, Value);
}
