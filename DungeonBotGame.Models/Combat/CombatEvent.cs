namespace DungeonBotGame.Models.Combat
{
    public record CombatEvent(int CombatTime, CharacterBase Character, CombatEventType CombatEventType);


    public record CombatEvent<TEventData>(int CombatTime, CharacterBase Character, CombatEventType CombatEventType, TEventData EventData) : CombatEvent(CombatTime, Character, CombatEventType);

    public enum CombatEventType
    {
        CharacterAction = 1,
        CooldownReset
    }
}
