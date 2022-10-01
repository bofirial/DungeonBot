namespace DungeonBotGame.Combat;

public record AdventureContext(AdventureMap AdventureMap, IAction LastAction, TimeSpan CombatTime);
