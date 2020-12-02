namespace DungeonBotGame
{
    public record CombatEffect(string Name, string ShortName, CombatEffectType CombatEffectType, short Value);

    public record PermanentCombatEffect(string Name, string ShortName, CombatEffectType CombatEffectType, short Value) :
        CombatEffect(Name, ShortName, CombatEffectType, Value);

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "CA1801:Review unused parameters", Justification = "These parameters are used.  Not sure why the tooling is causing a warning here.")]
    public record TimedIntervalCombatEffect(string Name, string ShortName, CombatEffectType CombatEffectType, short Value, int CombatTime, int CombatTimeInterval) :
        CombatEffect(Name, ShortName, CombatEffectType, Value);
}
