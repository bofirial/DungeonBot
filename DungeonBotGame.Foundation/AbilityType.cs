namespace DungeonBotGame.Foundation;

public enum AbilityType
{
    HeavySwing = 1,
    //AnalyzeSituation,
    //SurpriseAttack,
    //SalvageStrikes,

    //Repair,

    //LickWounds,
    //Swipe,
    //ProtectBabies
}

public static class AbilityTypeDetails
{
    public static List<AbilityType> TargettedAbilities = new()
    {
        AbilityType.HeavySwing
    };

    public static List<AbilityType> NonTargettedAbilities = new();
}
