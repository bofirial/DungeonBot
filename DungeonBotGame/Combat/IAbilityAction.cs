﻿using DungeonBotGame.Data;

namespace DungeonBotGame.Combat;

public interface IAbilityAction : IAction
{
    public AbilityType AbilityType { get; }
}
