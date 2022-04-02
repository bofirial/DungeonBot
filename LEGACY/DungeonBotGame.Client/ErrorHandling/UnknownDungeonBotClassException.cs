using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownDungeonBotClassException : InvalidOperationException
    {
        public UnknownDungeonBotClassException(DungeonBotClass dungeonBotClass) : base($"Unknown DungeonBot Class: {dungeonBotClass}") { }

        public UnknownDungeonBotClassException() { }

        public UnknownDungeonBotClassException(string message) : base(message) { }

        public UnknownDungeonBotClassException(string message, Exception innerException) : base(message, innerException) { }
    }
}

