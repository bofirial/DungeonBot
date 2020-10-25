using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownAbilityException : InvalidOperationException
    {
        public UnknownAbilityException(AbilityType abilityType) : base($"Unknown Ability Type: {abilityType}") { }

        public UnknownAbilityException() { }

        public UnknownAbilityException(string message) : base(message) { }

        public UnknownAbilityException(string message, Exception innerException) : base(message, innerException) { }
    }
}

