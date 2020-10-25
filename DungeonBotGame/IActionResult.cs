﻿namespace DungeonBotGame
{
    public interface IActionResult
    {
        public string DisplayText { get; }

        public ICharacter Character { get; }

        public IAction Action { get; }
    }
}