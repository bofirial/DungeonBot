﻿using System.Linq;
using DungeonBot.Models.Combat;

namespace DungeonBot.Client.BusinessLogic.EnemyActionModules
{
    public class WolfKingActionModule : IEnemyActionModule
    {
        public IAction Action(IActionComponent actionComponent, ISensorComponent sensorComponent)
        {
            var previousRoundResult = sensorComponent.EncounterRoundHistory.LastOrDefault();

            if (previousRoundResult != null && previousRoundResult.ActionResults.Any(a => a.Action is IAbilityAction))
            {
                return actionComponent.UseAbility(sensorComponent.Enemy, AbilityType.LickWounds);
            }

            return actionComponent.Attack(sensorComponent.DungeonBot);
        }
    }
}