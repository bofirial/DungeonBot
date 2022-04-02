using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownCharacterTypeException : InvalidOperationException
    {
        public UnknownCharacterTypeException() { }

        public UnknownCharacterTypeException(string message) : base(message) { }

        public UnknownCharacterTypeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

