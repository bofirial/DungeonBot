using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownActionTypeException : InvalidOperationException
    {
        public UnknownActionTypeException(ActionType actionType) : base($"Unknown Action Type: {actionType}") { }

        public UnknownActionTypeException() { }

        public UnknownActionTypeException(string message) : base(message) { }

        public UnknownActionTypeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

