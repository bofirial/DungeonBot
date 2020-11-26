namespace DungeonBotGame
{
    public record CombatEffect(string Name, CombatEffectType CombatEffectType, short Value, int? CombatTime, int? CombatTimeInterval);
}
