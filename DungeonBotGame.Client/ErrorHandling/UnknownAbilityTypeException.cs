using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownAbilityTypeException : InvalidOperationException
    {
        public UnknownAbilityTypeException(AbilityType abilityType) : base($"Unknown Ability Type: {abilityType}") { }

        public UnknownAbilityTypeException() { }

        public UnknownAbilityTypeException(string message) : base(message) { }

        public UnknownAbilityTypeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

