using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class AbilityNotAvailableException : InvalidOperationException
    {
        public AbilityNotAvailableException(string message) : base(message) { }

        public AbilityNotAvailableException(string message, Exception innerException) : base(message, innerException) { }

        public AbilityNotAvailableException() { }
    }
}
