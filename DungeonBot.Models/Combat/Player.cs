namespace DungeonBot.Models.Combat
{
    public class Player : CharacterBase, IPlayer
    {
        public Player(string dungeonBotName, int maximumHealth, ActionModuleContext actionModuleContext) : base(dungeonBotName, maximumHealth)
        {
            ActionModuleContext = actionModuleContext;
        }

        public ActionModuleContext ActionModuleContext { get; }
    }
}
