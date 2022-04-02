using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class InvalidTargetException : InvalidOperationException
    {
        public InvalidTargetException(ITarget? target) : base(target == null ? "Target is required" : $"Invalid target: {target}") { }

        public InvalidTargetException(string message) : base(message) { }

        public InvalidTargetException(string message, Exception innerException) : base(message, innerException) { }

        public InvalidTargetException() { }
    }
}
