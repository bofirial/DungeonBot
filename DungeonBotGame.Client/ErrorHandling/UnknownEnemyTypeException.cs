using System;

namespace DungeonBotGame.Client.ErrorHandling
{
    public class UnknownEnemyTypeException : InvalidOperationException
    {
        public UnknownEnemyTypeException(EnemyType enemyType) : base($"Unknown Enemy Type: {enemyType}") { }

        public UnknownEnemyTypeException() { }

        public UnknownEnemyTypeException(string message) : base(message) { }

        public UnknownEnemyTypeException(string message, Exception innerException) : base(message, innerException) { }
    }
}

