﻿namespace DungeonBotGame.Combat;

public enum CombatEffectType
{
    AttackPercentage = 1,
    ActionCombatTimePercentage,

    ImmediateAction,

    StunTarget,
    Stunned,

    DamageOverTime,

    SalvageStrikes,
    ProtectBabies
}
