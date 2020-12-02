namespace DungeonBotGame.Models.Combat
{
    public record CombatEvent(int CombatTime, CharacterBase Character, CombatEventType CombatEventType);

    public record CombatEffectCombatEvent(int CombatTime, CharacterBase Character, CombatEventType CombatEventType, CombatEffect CombatEffect) : CombatEvent(CombatTime, Character, CombatEventType);

    public record AbilityCombatEvent(int CombatTime, CharacterBase Character, CombatEventType CombatEventType, AbilityType AbilityType) : CombatEvent(CombatTime, Character, CombatEventType);
}
