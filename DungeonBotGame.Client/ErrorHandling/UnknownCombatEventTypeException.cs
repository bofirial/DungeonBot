using System;
using DungeonBotGame.Models.Combat;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownCombatEventTypeException : InvalidOperationException
    {
        public UnknownCombatEventTypeException(CombatEventType combatEventType) : base($"Unknown CombatEvent Type: {combatEventType}") { }

        public UnknownCombatEventTypeException() { }

        public UnknownCombatEventTypeException(string message) : base(message) { }

        public UnknownCombatEventTypeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

