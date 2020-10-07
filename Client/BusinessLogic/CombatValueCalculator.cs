namespace DungeonBot.Client.BusinessLogic
{
    public class CombatValueCalculator : ICombatValueCalculator
    {
        public int GetAttackValue(ICharacter sourceCharacter, ICharacter targetCharacter)
        {
            if (sourceCharacter.Name == "Hungry Dragon Whelp" || sourceCharacter.Name == "Wolf King")
            {
                return 15;
            }

            return 10;
        }

        public int GetAbilityValue(ICharacter sourceCharacter, ICharacter targetCharacter, AbilityType abilityType)
        {
            switch (abilityType)
            {
                case AbilityType.HeavySwing:
                    return GetAttackValue(sourceCharacter, targetCharacter) * 3;
                default:
                    throw new System.Exception($"Ability Unknown: {abilityType}");
            }
        }
    }
}
